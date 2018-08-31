using UnityEngine;

namespace SIS.Characters.Robo
{
	//Navigate to random room
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Start Wander")]
	public class StartWander : RoboPadronStateActions
	{
		public override void Execute(RoboPadron owner)
		{
			int roomAmount = owner.waypointNavigator.dungeon.RoomCount;
			int randomRoom = Random.Range(0, roomAmount);
			owner.waypointNavigator.SetWaypointGraph();
			owner.waypointNavigator.StartNavigation(randomRoom);
		}
	}
}