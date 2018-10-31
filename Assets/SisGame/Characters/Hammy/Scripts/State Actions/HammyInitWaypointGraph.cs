using UnityEngine;

namespace SIS.Characters.Ham
{
	//Navigate to random room
	[CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Init Waypoint Graph")]
	public class HammyInitWaypointGraph : HammyStateActions
	{
		public override void Execute(Hammy owner)
		{
			owner.waypointNavigator.SetWaypointGraph();
		}
	}
}