using UnityEngine;

namespace SIS.Characters.Robo
{
	//Navigate to random room
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Start Wander")]
	public class StartWander : RoboPadronStateActions
	{
		public override void Execute(RoboPadron owner)
		{
			owner.waypointNavigator.SetWaypointGraph();

			
		}
	}
}