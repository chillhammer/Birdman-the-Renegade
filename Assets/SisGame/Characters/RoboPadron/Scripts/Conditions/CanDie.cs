using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters.Robo
{
	[CreateAssetMenu(menuName = "Characters/RoboPadron/Conditions/Can Die")]
	public class CanDie : RoboPadronCondition
	{
		public override bool CheckCondition(RoboPadron owner)
		{
			return owner.health == 0;
		}
	}
}