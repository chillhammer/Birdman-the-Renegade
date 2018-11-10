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
			Vector3 position = owner.FinTransform.position + Vector3.up * 0.2f;
			Vector3 playerPosition = owner.playerTransform.value.position + Vector3.up * 0.2f;
			Vector3 direction = playerPosition - position;
			RaycastHit raycastHit;
			LayerMask mapAndPlayer = LayerMask.NameToLayer("Player") | LayerMask.NameToLayer("Map");
			Ray ray = new Ray(position, direction);
			Debug.DrawLine(position, playerPosition, Color.green);

			if (Mathf.Abs(Mathf.Acos(Vector3.Dot(direction.normalized, owner.mTransform.forward)) * Mathf.Rad2Deg) < 20)
			{
				if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, ~mapAndPlayer))
				{
					Debug.DrawLine(position, playerPosition, Color.green);
					return raycastHit.collider.tag == "Player";
				}
			}
			return false;
		}
	}
}