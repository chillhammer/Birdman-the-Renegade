using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters.Ham
{
	[CreateAssetMenu(menuName = "Characters/Hammy/Conditions/HammySetableCondition")]
	public class HammySetableCondition : HammyCondition
	{
		public bool Value;
		public override bool CheckCondition(Hammy owner)
		{
			return Value;
		}
	}
}