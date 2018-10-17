using UnityEngine;

namespace SIS.Characters.Robo
{
	//StateAction that allows for RoboPadron to move
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Can See Player")]
	public class CanSeePlayerAction : RoboPadronBTAction
	{

		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			if (owner.canSeePlayer)
			{
				return AivoTree.AivoTreeStatus.Success;
			}
			return AivoTree.AivoTreeStatus.Failure;
		}
	}
}