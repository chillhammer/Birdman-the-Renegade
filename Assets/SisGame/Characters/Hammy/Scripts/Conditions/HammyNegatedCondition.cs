using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters.Ham
{
	[CreateAssetMenu(menuName = "Characters/Hammy/Conditions/HammyNegatedCondition")]
	public class HammyNegatedCondition : HammyCondition
	{
		public HammyCondition NegatedCondition;
        public override bool CheckCondition(Hammy owner)
		{
			return !NegatedCondition.CheckCondition(owner);
		}
	}
}
