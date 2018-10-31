using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Items.Weapons;

namespace SIS.Characters.Sis
{
	public class IKAiming : MonoBehaviour
	{
		Animator anim;
		Sis owner;

		float handWeight;
		float lookWeight;
		float bodyWeight;

		Transform rhTarget;
		Transform lhTarget;
		bool updateLeftHand;
		public Transform shoulder;
		public Transform aimPivot;
		Vector3 lookDir;
		Weapon curWeapon;
		RecoilHandler recoilHandler;
		Vector3 basePosition;
		Vector3 baseRotation;
        Vector3 lBasePosition;
        Vector3 lBaseRotation;

		public void Init(Sis sis)
		{
			owner = sis;
			anim = owner.anim;

			if (shoulder == null)
				shoulder = anim.GetBoneTransform(HumanBodyBones.RightShoulder);

			CreateAimPivot();
			CreateRightHandTarget();
			CreateLeftHandTarget();
			//Setup Aim Position
			owner.movementValues.aimPosition = owner.mTransform.position + owner.mTransform.forward * 15;
			owner.movementValues.aimPosition.y += 1.4f;

			recoilHandler = new RecoilHandler();
		}

		
		//Update pivot and right hand
		private void OnAnimatorMove()
		{
			if (owner == null)
				return;
			lookDir = CaculateLookDirection();
			HandlePivot();
		}

		#region Right Hand and Lef Hand Target is on Weapon
		//Creates empty object for right hand target
		void CreateRightHandTarget()
		{
			rhTarget = new GameObject().transform;
			rhTarget.name = "Right Hand Target";
			rhTarget.parent = aimPivot;
		}

		void CreateLeftHandTarget()
		{
			lhTarget = new GameObject().transform;
			lhTarget.name = "Left Hand Target";
			lhTarget.parent = aimPivot;
		}

		//For When Weapon Loads in and Changes Top Half Layer
		void SetAnimLayerWeight(string animationLayer, float weight)
		{
			int layerIndex = anim.GetLayerIndex(animationLayer);
			if (layerIndex >= 0)
			{
				anim.SetLayerWeight(layerIndex, weight);
			} else
			{
				Debug.LogWarning("Cannot find animation layer: " + animationLayer);
			}
		}

		//Sets right hand target ontop of weapon
		//Activate method whenever you change weapons
		public void UpdateWeaponAiming(Weapon w)
		{
			if (curWeapon != null) { //Turn Off Weapon Animation Layer
				SetAnimLayerWeight(curWeapon.topHalfAnimatorLayerName, 0);
			}

			curWeapon = w;

			if (w == null)
				return;
			//Turn On Weapon Animation Layer
			SetAnimLayerWeight(curWeapon.topHalfAnimatorLayerName, 1);

			//Update so right hand is holding weapon
			rhTarget.localPosition = w.holdingPosition.value;
			rhTarget.localEulerAngles = w.holdingEulers.value;

			//Update so right hand is holding weapon
			updateLeftHand = w.IKLeftHand;
			if (updateLeftHand)
			{
				lhTarget.localPosition = w.leftHoldingPosition.value;
				lhTarget.localEulerAngles = w.leftHoldingEulers.value;
			}

			//Store local transform for recoil
			basePosition = rhTarget.localPosition;
			baseRotation = rhTarget.localEulerAngles;
            lBasePosition = lhTarget.localPosition;
            lBaseRotation = lhTarget.localEulerAngles;
		}
		#endregion

		#region Pivot towards Look Direction
		//Creates Aiming Pivot
		void CreateAimPivot()
		{
			aimPivot = new GameObject().transform;
			aimPivot.name = "Aim Pivot";
			aimPivot.parent = owner.transform;
		}

		//Handles Transform
		void HandlePivot()
		{
			HandlePivotPosition();
			HandlePivotRotation();
		}

		//Pivot around shoulder
		void HandlePivotPosition()
		{
			aimPivot.position = shoulder.position;
		}

		//Rotate towards aiming target
		void HandlePivotRotation()
		{
			float speed = 15;
			Vector3 targetDir = lookDir;
			if (targetDir == Vector3.zero)
				targetDir = aimPivot.forward;
			Quaternion tr = Quaternion.LookRotation(targetDir);
			aimPivot.rotation = Quaternion.Slerp(aimPivot.rotation, tr, owner.delta * speed);
		}

		//Calculates the direction to aim towards
		private Vector3 CaculateLookDirection()
		{
			return owner.movementValues.aimPosition - owner.mTransform.position;
		}
		#endregion

		//Uses Mecanim's IK feature to look at aimPosition
		private void OnAnimatorIK()
		{
			if (owner == null) {
				Debug.Log("Owner is null in IK Aiming");
				return;
			}

			HandleWeights();

			anim.SetLookAtWeight(lookWeight, bodyWeight, 1, 1, 1);
			anim.SetLookAtPosition(owner.movementValues.aimPosition);


			UpdateIK(AvatarIKGoal.RightHand, rhTarget, handWeight);
			if (updateLeftHand)
				UpdateIK(AvatarIKGoal.LeftHand, lhTarget, handWeight);
		}

		//Tunes IK weights based on a number of factors
		void HandleWeights()
		{
			//targets to lerp to
			float targetLookWeight = 1;
			float targetHandWeight = 0;

			//Intensity if aiming down sights
			if (owner.isGunReady)
				targetHandWeight = 0.5f;
			if (owner.isShooting)
				targetHandWeight = 1;
			if (owner.isAiming) {
				targetHandWeight = 1;
				bodyWeight = 0.4f;
			}
			else
				bodyWeight = 0.3f;
			if (owner.isReloading)
				targetHandWeight = 0;

			//Constraints on IK for looking at sharp angles
			float angle = Vector3.Angle(owner.mTransform.forward, lookDir);
			if (angle > 45)
				targetLookWeight = 0;

			if (angle > 60)
				targetHandWeight = 0;

			//Dead
			if (owner.isDead)
			{
				targetHandWeight = 0f;
				bodyWeight = 0f;
				targetLookWeight = 0f;
			}

			//Smoothly Change Weights
			lookWeight = Mathf.Lerp(lookWeight, targetLookWeight, owner.delta);
			handWeight = Mathf.Lerp(handWeight, targetHandWeight, 9 * owner.delta);

		}
		#region Recoil
		//Start the recoil process
		public void StartRecoil()
		{
			recoilHandler.StartRecoil(curWeapon.recoilLength);
		}

		//Tick the recoil and update offset positions
		public void HandleRecoil()
		{
			recoilHandler.Tick(curWeapon, owner.delta);
			rhTarget.localPosition = basePosition + recoilHandler.OffsetPosition;
			rhTarget.localEulerAngles = baseRotation + recoilHandler.OffsetRotation;
            if (curWeapon.IKLeftHand) {
                lhTarget.localPosition = lBasePosition + recoilHandler.OffsetPosition;
                lhTarget.localEulerAngles = lBaseRotation + recoilHandler.OffsetRotation;
            }
		}
		#endregion

		//Helper Functions
		private void UpdateIK(AvatarIKGoal goal, Transform t, float w)
		{
			anim.SetIKPositionWeight(goal, w);
			anim.SetIKRotationWeight(goal, w);
			anim.SetIKPosition(goal, t.position);
			anim.SetIKRotation(goal, t.rotation);
		}
	}
}
