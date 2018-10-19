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

		public int PatrolLength = 1;
        public override AivoTreeStatus Act(float timeTick, Hammy owner)
        {
			owner.patrolRoute.Clear();
			owner.patrolIndex = 0;
			int roomAmount = owner.waypointNavigator.dungeon.RoomCount;
			int randomRoom = Random.Range(0, roomAmount);
			owner.patrolRoute.Add(randomRoom);
			owner.waypointNavigator.StartNavigation(randomRoom);
			return AivoTreeStatus.Success;
        }
	}
}

