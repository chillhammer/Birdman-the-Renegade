using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters.Ham
{
	[CreateAssetMenu(menuName = "Characters/Hammy/Conditions/HammyWithinRangeOfPlayer")]
	public class HammyWithinRangeOfPlayer : HammyCondition
	{
		public float Range = 50f;
		public override bool CheckCondition(Hammy owner)
		{
			Vector3 position = owner.mTransform.position;
			Vector3 playerPosition = owner.playerTransform.value.position;
			Vector3 distanceVector = playerPosition - position;
			distanceVector.y = 0;
			return distanceVector.magnitude < Range;
		}
	}
}
