using UnityEngine;
using UnityEditor;
using SIS.States.Actions;

namespace SIS.Characters.Robo
{
	//Navigate to random room
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Move Animations")]
	public class MoveAnimations : RoboPadronStateActions
	{
		public string forwardAnimatorText = "Vertical";
		public string aimingAnimatorText = "Aiming";

		public float gunMoveSpeed = 0.5f;

		public override void Execute(RoboPadron owner)
		{

			if (owner.anim == null)
				return;

			owner.anim.SetFloat(forwardAnimatorText, owner.rigid.velocity.magnitude, 0.1f, owner.delta);
			owner.anim.SetBool(aimingAnimatorText, owner.isAiming);

			if (owner.gunModel != null)
			{
				if (!owner.isAiming)
				{
					owner.gunModel.localPosition = Vector3.Lerp(owner.gunModel.localPosition, 
						new Vector3(0.165f, -0.44f, -0.037f), gunMoveSpeed * owner.delta);

					owner.gunModel.localRotation = Quaternion.Slerp(owner.gunModel.localRotation,
						Quaternion.Euler(new Vector3(33.174f, 2.666f, -140.91f)), gunMoveSpeed * owner.delta);
				} else
				{
					owner.gunModel.localPosition = Vector3.Lerp(owner.gunModel.localPosition,
						new Vector3(0.434f, -0.104f, 0.091f), gunMoveSpeed * owner.delta);

					owner.gunModel.localRotation = Quaternion.Slerp(owner.gunModel.localRotation,
						Quaternion.Euler(new Vector3(48.13f, 18.673f, -70.551f)), gunMoveSpeed * owner.delta);
				}
			}
		}
	}
}