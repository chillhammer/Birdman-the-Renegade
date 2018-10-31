using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters.Ham
{
	[CreateAssetMenu(menuName = "Characters/Hammy/Conditions/HammyAndCondition")]
	public class HammyAndCondition : HammyCondition
	{
		public HammyCondition[] HammyConditions;
        public override bool CheckCondition(Hammy owner)
		{
			foreach (HammyCondition cond in HammyConditions)
			{
				if (!cond.CheckCondition(owner)) return false;
			}
			return true;
		}
	}
}
