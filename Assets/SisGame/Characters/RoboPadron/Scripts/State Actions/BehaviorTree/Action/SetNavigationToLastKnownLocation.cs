using UnityEngine;

namespace SIS.Characters.Robo
{
	//StateAction that allows for RoboPadron to move
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Set Navigation To Last Known Location")]
	public class SetNavigationToLastKnownLocation : RoboPadronBTAction
	{

		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			Debug.Log("Setting navigation to last known location");
			owner.waypointNavigator.StartNavigation(owner.playerLastKnownLocation.position);

			return AivoTree.AivoTreeStatus.Success;
		}
	}
}