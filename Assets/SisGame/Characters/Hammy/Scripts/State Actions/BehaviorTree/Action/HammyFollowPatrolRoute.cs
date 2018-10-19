using System.Collections;
using System.Collections.Generic;
using AivoTree;
using UnityEngine;

namespace SIS.Characters.Ham
{
	[CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Behavior Tree/HammyFollowPatrolRoute")]
	public class HammyFollowPatrolRoute : HammyBTAction
	{
		public float moveSpeed = 3f;
		public float turnSpeed = 2f;

        public override AivoTreeStatus Act(float timeTick, Hammy owner)
        {
			owner.rigid.velocity = Vector3.zero;
			if (owner.waypointNavigator.PathFinished)
			{
				int nextRoomIndex = owner.GetNextPatrolRoomIndex();
				if (nextRoomIndex == -1) return AivoTreeStatus.Success;
				owner.waypointNavigator.StartNavigation(nextRoomIndex);
			}

			Vector3 direction = owner.waypointNavigator.DirectionToWaypoint;

			owner.mTransform.rotation = Quaternion.Slerp(owner.mTransform.rotation,
				Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)), turnSpeed * owner.delta);

			Vector3 motion = owner.mTransform.forward * moveSpeed * owner.delta;
			owner.rigid.velocity = motion;
			return AivoTree.AivoTreeStatus.Running;
        }
	}
}