using UnityEngine;

namespace SIS.Characters.Robo
{
	//StateAction that allows for RoboPadron to move
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Is Player Dead")]
	public class IsPlayerDeadAction : RoboPadronBTAction
	{
		public Sis.SisVariable sisVariable;
		public override AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner)
		{
			if (sisVariable.value == null || sisVariable.value.isDead)
			{
				return AivoTree.AivoTreeStatus.Success;
			}
			return AivoTree.AivoTreeStatus.Failure;
		}
	}
}