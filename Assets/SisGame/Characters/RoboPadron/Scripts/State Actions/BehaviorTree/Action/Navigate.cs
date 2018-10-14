using UnityEngine;

namespace SIS.Characters.Robo
{
	//StateAction that allows for RoboPadron to move
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Navigate")]
	public class Navigate : RoboPadronBTAction
	{
		public float moveSpeed = 3f;
		public float turnSpeed = 2f;

		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			owner.rigid.velocity = Vector3.zero;
			if (owner.waypointNavigator.PathFinished)
				return AivoTree.AivoTreeStatus.Success;

			Vector3 direction = owner.waypointNavigator.DirectionToWaypoint;

			owner.mTransform.rotation = Quaternion.Slerp(owner.mTransform.rotation,
				Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)), turnSpeed * owner.delta);


			Vector3 motion = owner.mTransform.forward * moveSpeed * owner.delta;
			//owner.rigid.AddForce(motion);
			owner.rigid.velocity = motion;
			//owner.mTransform.position = owner.mTransform.position + motion;


			return AivoTree.AivoTreeStatus.Running;
		}
	}
}