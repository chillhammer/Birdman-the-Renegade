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
		private float time = 0;
		private Vector3 lastPosition;
        public override AivoTreeStatus Act(float timeTick, Hammy owner)
        {
			if ((lastPosition - owner.mTransform.position).magnitude < 0.2) { // Fail Safe
				time += Time.deltaTime;
				if (time > 3) {
					Debug.Log("Reset Path");
					return AivoTreeStatus.Success;
				}
			} else {
				time = 0;
			}
			lastPosition = owner.mTransform.position;
			owner.rigid.velocity = Vector3.zero;
			if (owner.waypointNavigator.PathFinished)
			{
				int nextRoomIndex = owner.GetNextPatrolRoomIndex();
				if (nextRoomIndex == -1) return AivoTreeStatus.Success;
				owner.waypointNavigator.StartNavigation(nextRoomIndex);
			}

			Vector3 direction = owner.waypointNavigator.DirectionToWaypoint;
			direction.y = 0.4f - owner.mTransform.position.y;
			owner.mTransform.rotation = Quaternion.Slerp(owner.mTransform.rotation,
				Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);

			Vector3 motion = owner.mTransform.forward * moveSpeed * Time.deltaTime;
			owner.rigid.velocity = motion;
			return AivoTree.AivoTreeStatus.Running;
        }
	}
}