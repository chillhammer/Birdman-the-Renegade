using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using SIS.States;

namespace SIS.Characters.Sis
{
	// Rotate to move direction so you run in that direction. For non-aiming state
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Rotate On Move Direction")]
	public class RotateOnMoveDirection : SisStateActions
	{
		public float speed = 8;

		public override void Execute(Sis owner)
		{
			//Get Move Direction
			Vector3 targetDir = owner.movementValues.moveDirection;

			if (targetDir == Vector3.zero)
				targetDir = owner.mTransform.forward;

			//Get Speed of Rotation
			float moveAmount = owner.movementValues.moveAmount;
			float rotateAmount = moveAmount * speed * owner.delta;

			//Rotate Transform Towards Move Direction
			Quaternion targetRotation = Quaternion.LookRotation(targetDir);
			owner.mTransform.rotation = Quaternion.Slerp(owner.mTransform.rotation, targetRotation, rotateAmount);
		}
	}
}
