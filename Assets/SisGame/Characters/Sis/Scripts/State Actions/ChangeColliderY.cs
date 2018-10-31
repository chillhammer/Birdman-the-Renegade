using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.States;

namespace SIS.Characters.Sis
{
	//Handles Movement based on object transform direction
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Change Collider Y")]
	public class ChangeColliderY : SisStateActions
	{
		public float newY;

		public override void Execute(Sis owner)
		{
			CapsuleCollider collider = owner.GetComponent<CapsuleCollider>();
			Vector3 center = collider.center;
			center.y = newY;
			collider.center = center;
		}
	}
}
