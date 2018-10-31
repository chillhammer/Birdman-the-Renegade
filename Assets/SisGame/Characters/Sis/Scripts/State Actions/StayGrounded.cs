using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.States;

namespace SIS.Characters.Sis
{
	//Stick to ground within range
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/StayGrounded")]
	public class StayGrounded : SisStateActions
	{
		public float snapRange = 0.7f; //Up to dist away to snap to ground
		public LayerMask ignoreLayers;
		public override void Execute(Sis owner)
		{
			Vector3 origin = owner.mTransform.position;
			//origin.y += snapRange;
			Vector3 dir = -Vector3.up;
			float dist = snapRange; //Checks After origin has been pushed up
			RaycastHit hit;
			if (!Physics.Raycast(origin, dir, out hit, dist, ignoreLayers.value))
			{
				/*Vector3 targetPosition = hit.point;
				targetPosition.x = owner.mTransform.position.x;
				targetPosition.z = owner.mTransform.position.z;
				owner.mTransform.position = targetPosition; //origin must be at bottom
				*/
				origin.y -= owner.delta;
				owner.mTransform.position = origin;
			}
			Debug.DrawLine(origin, origin + dir * dist, Color.red);
		}
	}
}
