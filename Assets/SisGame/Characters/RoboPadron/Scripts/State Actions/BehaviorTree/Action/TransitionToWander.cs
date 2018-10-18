using UnityEngine;

namespace SIS.Characters.Robo
{
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Transition to Wander")]
	public class TransitionToWander : RoboPadronBTAction
	{
		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			owner.transitionToWander = true;
			return AivoTree.AivoTreeStatus.Success;
		}
	}
}