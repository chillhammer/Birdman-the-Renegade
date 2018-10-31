using UnityEngine;

namespace SIS.Characters.Robo
{
	//StateAction that allows for RoboPadron to move
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Stop Navigate")]
	public class StopNavigate : RoboPadronBTAction
	{
		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			//Debug.Log("Stopping Navigation");
			owner.waypointNavigator.StopNavigate();
			return AivoTree.AivoTreeStatus.Success;
		}
	}
}