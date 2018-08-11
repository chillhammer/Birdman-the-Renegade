using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.States;

namespace SIS.Characters.Sis
{
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Check Aiming")]
	public class CheckAiming : SisCondition
	{
		public bool reversed;

		public override bool CheckCondition(Sis owner)
		{
			if (owner)
			{
				if (reversed)
					return !owner.isAiming;
				return owner.isAiming;
			}
			return false;
		}
	}
}