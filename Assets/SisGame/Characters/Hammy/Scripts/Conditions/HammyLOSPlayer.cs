using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters.Ham
{
	[CreateAssetMenu(menuName = "Characters/Hammy/Conditions/HammyLOSPlayer")]
	public class HammyLOSPlayer : HammyCondition
	{
		public override bool CheckCondition(Hammy owner)
		{
			Vector3 position = owner.FinTransform.position;
			Vector3 playerPosition = owner.playerTransform.value.position;
			Vector3 direction = playerPosition - position;
			RaycastHit raycastHit;
			LayerMask mapAndPlayer = LayerMask.NameToLayer("Player") | LayerMask.NameToLayer("Map");
			Ray ray = new Ray(position, direction);
			if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, ~mapAndPlayer))
			{
				return raycastHit.collider.tag == "Player";
			}
			return false;
		}
	}
}