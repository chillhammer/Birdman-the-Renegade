using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters.Robo
{
	[CreateAssetMenu(menuName = "Characters/RoboPadron/Conditions/Check Behavior")]
	public class ConditionBTNode : RoboPadronCondition
	{
		public RoboPadronBTNode node;
		public RoboPadronBTNode rootToReset;
		public override bool CheckCondition(RoboPadron owner)
		{
			if (node.Tick(owner.delta, owner) == AivoTree.AivoTreeStatus.Success)
			{
				if (rootToReset != null)
				{
					Debug.Log("Resetting Root");
					rootToReset.Reset(owner);
				}
				return true;
			}
			return false;
		}
	}
}