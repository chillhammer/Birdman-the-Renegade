using System.Collections;
using System.Collections.Generic;
using AivoTree;
using UnityEngine;

namespace SIS.Characters.Ham
{
	[CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Behavior Tree/HammyFollowPatrolRoute")]
	public class HammyFollowPatrolRoute : HammyBTAction
	{
		public SO.FloatVariable turnSpeed;
		public bool fleeSpeedBoost = false;
		public SO.TransformVariable sisTransform;
		private float time = 0;
		private Vector3 lastPosition;
        public override AivoTreeStatus Act(float timeTick, Hammy owner)
        {
			/*
			if ((lastPosition - owner.mTransform.position).magnitude < 0.02) { // Fail Safe
				time += Time.deltaTime;
				if (time > 3) {
					Debug.Log("Reset Path");
					int roomIndex = Random.Range(0, owner.waypointNavigator.dungeon.RoomCount);
					owner.waypointNavigator.StartNavigation(roomIndex);
				}
			} else {
				time = 0;
			}
			*/
			lastPosition = owner.mTransform.position;
			owner.rigid.velocity = Vector3.zero;
			if (owner.waypointNavigator.PathFinished)
			{
				owner.wasHit = false;
				int nextRoomIndex = owner.GetNextPatrolRoomIndex();
				if (nextRoomIndex == -1) return AivoTreeStatus.Success;
				owner.waypointNavigator.StartNavigation(nextRoomIndex);
			}

			Vector3 direction = owner.waypointNavigator.DirectionToWaypoint;
			direction.y = 0.4f - owner.mTransform.position.y;
			float curMoveSpeed = (fleeSpeedBoost ? owner.fleeSpeed.value : owner.moveSpeed.value);
			//Sharp Turns
			/*if (Vector3.Angle(direction.normalized, owner.mTransform.forward) > 90)
			{
				curMoveSpeed *= 0.8f;
				//Debug.Log("Sharp Angle!");
				owner.mTransform.rotation = Quaternion.Slerp(owner.mTransform.rotation,
				 	Quaternion.LookRotation(direction), 5 * Time.deltaTime);
			}
			else */if (Vector3.Angle(direction.normalized, owner.mTransform.forward) > 0.075) {
				curMoveSpeed *= 0.8f;
				Vector3 rot = direction - owner.mTransform.forward;
				// owner.forward *= rot * turnSpeed * Time.deltaTime;
				owner.transform.rotation =
					Quaternion.RotateTowards(owner.transform.rotation, Quaternion.LookRotation(direction), 
					turnSpeed.value * (fleeSpeedBoost ? 4 : 1));
				// owner.mTransform.rotation = Quaternion.Lerp(owner.mTransform.rotation,
				// 	Quaternion.LookRotation(direction), turnSpeed * Time.deltaTime);
			}

			if (!fleeSpeedBoost)
			{
				if (owner.wasHit)
				{
					owner.wasHit = false;
					owner.waypointNavigator.StartNavigation(sisTransform.value.position);
				}
			}

			Vector3 motion = owner.mTransform.forward * curMoveSpeed;
			owner.rigid.velocity = motion;
			return AivoTree.AivoTreeStatus.Running;
        }
	}
}