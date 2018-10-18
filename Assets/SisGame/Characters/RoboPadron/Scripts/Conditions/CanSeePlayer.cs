using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters.Robo
{
	[CreateAssetMenu(menuName = "Characters/RoboPadron/Conditions/Can See Player")]
	public class CanSeePlayer : RoboPadronCondition
	{
		public RoboPadronBTNode wanderRoot;
		public override bool CheckCondition(RoboPadron owner)
		{
			if (owner.canSeePlayer)
			{
				if (wanderRoot != null)
				{
					Debug.Log("Resetting Wander Root");
					wanderRoot.Reset(owner);
				}
			}
			return owner.canSeePlayer;
		}
	}
}