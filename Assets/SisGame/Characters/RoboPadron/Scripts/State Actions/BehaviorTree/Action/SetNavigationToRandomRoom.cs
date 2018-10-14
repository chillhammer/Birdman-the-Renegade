using UnityEngine;

namespace SIS.Characters.Robo
{
	//StateAction that allows for RoboPadron to move to random room
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Set Navigation To Random Room")]
	public class SetNavigationToRandomRoom : RoboPadronBTAction
	{
		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			int roomAmount = owner.waypointNavigator.dungeon.RoomCount;
			int randomRoom = Random.Range(0, roomAmount);
			owner.waypointNavigator.StartNavigation(randomRoom);

			return AivoTree.AivoTreeStatus.Success;
		}
	}
}