using UnityEngine;

namespace SIS.Characters.Robo
{
	//StateAction that allows for RoboPadron to move
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Set Navigation To Last Known Room")]
	public class SetNavigationToLastKnownRoom : RoboPadronBTAction
	{
		public Map.Dungeon dungeon;
		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			Vector3 pos = owner.playerLastKnownLocation.position;
			Room room = dungeon.GetRoom((int)pos.x, (int)pos.z);
			int roomIndex = dungeon.GetRoomIndex(room);
			if (roomIndex == -1)
				roomIndex = dungeon.GetClosestRoomIndex(pos);

			owner.waypointNavigator.StartNavigation(roomIndex);

			//Debug.Log("Setting navigation to last known room");

			return AivoTree.AivoTreeStatus.Success;
		}
	}
}