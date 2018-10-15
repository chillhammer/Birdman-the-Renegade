using UnityEngine;

namespace SIS.Characters.Robo
{
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Rotate Head")]
	public class RotateHead : RoboPadronBTAction
	{
		[Range(-180, 180)]
		public float angleTarget;

		public float rotateSpeed = 5;

		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			//Change X Rotation of Head
			float current = owner.headBone.transform.localRotation.eulerAngles.x;
			Vector3 eulers = owner.headBone.transform.localRotation.eulerAngles;
			eulers.x = Mathf.MoveTowardsAngle(current, angleTarget, rotateSpeed * timeTick);

			owner.headBone.localRotation = Quaternion.Euler(eulers);

			//Debug.Log("current = " + current + " and target = " + angleTarget);
			//Debug.Log("Delta angle is " + Mathf.DeltaAngle(current, angleTarget));
			if (Mathf.Abs(Mathf.DeltaAngle(current, angleTarget)) < 1f)
			{
				//Debug.Log("Head angle reached target!");
				return AivoTree.AivoTreeStatus.Success;
			}
			return AivoTree.AivoTreeStatus.Running;
		}
	}
}