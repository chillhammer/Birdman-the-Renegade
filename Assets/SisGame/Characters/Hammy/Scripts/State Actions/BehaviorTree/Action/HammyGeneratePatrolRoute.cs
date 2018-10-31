using System.Collections;
using System.Collections.Generic;
using AivoTree;
using UnityEngine;
using SIS.Map;

namespace SIS.Characters.Ham
{
	[CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Behavior Tree/HammyGeneratePatrolRoute")]
	public class HammyGeneratePatrolRoute : HammyBTAction
	{

		public int PatrolLength = 1;
        public override AivoTreeStatus Act(float timeTick, Hammy owner)
        {
			owner.patrolRoute.Clear();
			owner.patrolIndex = 0;

			Vector3 position = owner.transform.position;
			Dungeon dungeon = owner.waypointNavigator.dungeon;

			int startRoomIndex = dungeon.GetClosestRoomIndex((int) position.x, (int) position.y);
			Debug.Log(startRoomIndex);
			owner.patrolRoute.Add(startRoomIndex);
			// random for now.. need to create a proper connected route.
			for (int i = 0; i < PatrolLength; i++)
			{
				int roomAmount = dungeon.RoomCount;
				int randomRoom = Random.Range(0, roomAmount);
				owner.patrolRoute.Add(randomRoom);
			}
			owner.waypointNavigator.StartNavigation(startRoomIndex);
			return AivoTreeStatus.Success;
        }
	}
}
