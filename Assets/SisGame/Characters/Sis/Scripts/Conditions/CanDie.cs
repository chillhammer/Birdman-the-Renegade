using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters.Sis
{
	[CreateAssetMenu(menuName = "Characters/Sis/Conditions/Can Die")]
	public class CanDie : SisCondition
	{
		public override bool CheckCondition(Sis owner)
		{
			return owner.health <= 0;
		}
	}
}