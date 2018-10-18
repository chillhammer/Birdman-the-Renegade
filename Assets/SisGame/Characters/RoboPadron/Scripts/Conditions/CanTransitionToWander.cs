using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters.Robo
{
	[CreateAssetMenu(menuName = "Characters/RoboPadron/Conditions/Check Transition to Wander")]
	public class CanTransitionToWander : RoboPadronCondition
	{
		public override bool CheckCondition(RoboPadron owner)
		{
			bool temp = owner.transitionToWander;
			owner.transitionToWander = false;
			return temp;
		}
	}
}