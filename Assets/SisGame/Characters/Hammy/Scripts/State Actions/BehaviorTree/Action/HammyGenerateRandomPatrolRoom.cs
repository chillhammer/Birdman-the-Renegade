using System.Collections;
using System.Collections.Generic;
using AivoTree;
using UnityEngine;
using SIS.Map;

namespace SIS.Characters.Ham
{
	[CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Behavior Tree/HammyGenerateRandomPatrolRoom")]
	public class HammyGenerateRandomPatrolRoom : HammyBTAction
	{

		private int prevInd = -1;
        public override AivoTreeStatus Act(float timeTick, Hammy owner)
        {
			owner.patrolRoute.Clear();
			owner.patrolIndex = 0;
			int roomAmount = owner.waypointNavigator.dungeon.RoomCount;
			Vector3 playerPos = owner.playerTransform.value.position;
			float max_dist = float.MinValue;
			int max_ind = -1;
			for (int i = 0; i < roomAmount; i++) {
				float distance = Vector3.Distance(
					owner.waypointNavigator.dungeon.Rooms[i].rect.position, playerPos);
				if (prevInd != i) {
					max_ind = distance > max_dist ? i : max_ind;
					max_dist = distance > max_dist ? distance : max_dist;
				}
			}
			prevInd = max_ind;
			owner.patrolRoute.Add(max_ind);
			owner.waypointNavigator.StartNavigation(max_ind);
			return AivoTreeStatus.Success;
        }
	}
}

