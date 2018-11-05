using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters.Robo
{
	[CreateAssetMenu(menuName = "Characters/RoboPadron/Conditions/Can Start Shooting")]
	public class CanStartShooting : RoboPadronCondition
	{
		public RoboPadronBTNode inShootingRange;
		public RoboPadronBTNode fightRoot;
		public override bool CheckCondition(RoboPadron owner)
		{

			if (inShootingRange.Tick(owner.delta, owner) == AivoTree.AivoTreeStatus.Success
				&& owner.canSeePlayer)
			{
				
				if (fightRoot != null)
				{
					//Debug.Log("Resetting Fight Root");
					fightRoot.Reset(owner);
				}
				return true;
			}
			return false;
		}
	}
}