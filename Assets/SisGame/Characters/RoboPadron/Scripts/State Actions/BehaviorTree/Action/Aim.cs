using UnityEngine;

namespace SIS.Characters.Robo
{
	//RoboPadron can rotate towards player!
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Aim")]
	public class Aim : RoboPadronBTAction
	{
		public SO.TransformVariable playerTransform;
		public float headRotateSpeed = 10;
		public float bodyRotateSpeed = 100;
		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			//Towards Player
			Vector3 dir = (playerTransform.value.position - owner.mTransform.position).normalized;
			dir.y = 0;
			Quaternion towardsPlayer = Quaternion.LookRotation(dir, Vector3.up);

			//Quaternion headRot = owner.headBone.rotation;
			owner.mTransform.rotation = Quaternion.Slerp(owner.mTransform.rotation, towardsPlayer, 
				bodyRotateSpeed * owner.delta);
			Quaternion newHeadRot = owner.headBone.transform.localRotation;
			newHeadRot = Quaternion.Slerp(newHeadRot, Quaternion.identity, headRotateSpeed * owner.delta);

			owner.headBone.localRotation = newHeadRot;

			if (Mathf.Abs(Quaternion.Angle(owner.mTransform.rotation, towardsPlayer)) < 1f)
			{
				//Debug.Log("Aiming successful!");
				return AivoTree.AivoTreeStatus.Success;
			}
			return AivoTree.AivoTreeStatus.Running;
		}
	}
}