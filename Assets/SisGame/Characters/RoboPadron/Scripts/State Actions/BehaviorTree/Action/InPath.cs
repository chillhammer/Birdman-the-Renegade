using UnityEngine;

namespace SIS.Characters.Robo
{
	//Checks if it is in path
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/In Path")]
	public class InPath : RoboPadronBTAction
	{

		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			if (!owner.waypointNavigator.PathFinished)
			{
				return AivoTree.AivoTreeStatus.Success;
			}
			return AivoTree.AivoTreeStatus.Failure;
		}
	}
}