using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu(menuName = "Actions/State Actions/Grounded")]
	public class Grounded : StateActions
	{
		public float snapRange = 0.7f; //Up to dist away to snap to ground
		public override void Execute(StateManager stateManager)
		{
			Vector3 origin = stateManager.transform.position;
			origin.y += snapRange;
			Vector3 dir = -Vector3.up;
			float dist = snapRange * 2; //Checks After origin has been pushed up
			RaycastHit hit;
			if (Physics.Raycast(origin, dir, out hit, dist, stateManager.ignoreLayers))
			{
				Vector3 targetPosition = hit.point;
				targetPosition.x = stateManager.mTransform.position.x;
				targetPosition.z = stateManager.mTransform.position.z;
				stateManager.mTransform.position = targetPosition; //origin must be at bottom
			}
		}
	}
}
