using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using SIS.States;

namespace SIS.Characters.Sis
{
	// Rotate to align with look direction
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/StopMoving")]
	public class StopMoving : SisStateActions
	{

		public override void Execute(Sis owner)
		{
			owner.rigid.velocity = Vector3.zero;
		}
	}
}
