using UnityEngine;
using UnityEditor;
using SIS.States.Actions;

namespace SIS.Characters.Robo
{
	//Navigate to random room
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Move Wander")]
	public class MoveWander : RoboPadronStateActions
	{
		public float moveSpeed = 3f;
		public float turnSpeed = 2f;

		public override void Execute(RoboPadron owner)
		{
			owner.rigid.velocity = Vector3.zero;
			if (owner.waypointNavigator.PathFinished)
				return;

			Vector3 direction = owner.waypointNavigator.DirectionToWaypoint;

			owner.mTransform.rotation = Quaternion.Slerp(owner.mTransform.rotation,
				Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)), turnSpeed * owner.delta);


			Vector3 motion = owner.mTransform.forward * moveSpeed * owner.delta;
			//owner.rigid.AddForce(motion);
			owner.rigid.velocity = motion;
			//owner.mTransform.position = owner.mTransform.position + motion;
		}
	}
}