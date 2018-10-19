using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters.Robo
{
	[CreateAssetMenu(menuName = "Characters/RoboPadron/Conditions/Check Transition to Wander")]
	public class CanTransitionToWander : RoboPadronCondition
	{
		public RoboPadronBTNode fightRoot;
		public override bool CheckCondition(RoboPadron owner)
		{
			if (owner.transitionToWander)
			{
				if (fightRoot != null)
				{
					Debug.Log("Resetting Fight Root");
					fightRoot.Reset(owner);
				}
			}
			bool temp = owner.transitionToWander;
			owner.transitionToWander = false;
			return temp;
		}
	}
}