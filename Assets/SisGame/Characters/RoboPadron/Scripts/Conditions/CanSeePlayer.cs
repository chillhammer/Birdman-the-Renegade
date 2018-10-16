using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters.Robo
{
	[CreateAssetMenu(menuName = "Characters/RoboPadron/Conditions/Can See Player")]
	public class CanSeePlayer : RoboPadronCondition
	{
		public override bool CheckCondition(RoboPadron owner)
		{
			return owner.canSeePlayer;
		}
	}
}