using UnityEngine;

namespace SIS.Characters.Robo
{
	//Navigate to random room
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Init Waypoint Graph")]
	public class InitWaypointGraph : RoboPadronStateActions
	{
		public override void Execute(RoboPadron owner)
		{
			owner.waypointNavigator.SetWaypointGraph();
		}
	}
}