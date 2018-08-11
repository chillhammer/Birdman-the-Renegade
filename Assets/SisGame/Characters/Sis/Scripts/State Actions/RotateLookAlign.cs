using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using SIS.States;

namespace SIS.Characters.Sis
{
	// Rotate to align with look direction
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Rotate Align with Look Direction")]
	public class RotateLookAlign : SisStateActions
	{
		public float speed = 8;

		public override void Execute(Sis owner)
		{
			//Get direction of looking
			Vector3 targetDir = owner.movementValues.lookDirection;
			if (targetDir == Vector3.zero)
				targetDir = owner.mTransform.forward;

			Quaternion targetRotation = Quaternion.LookRotation(targetDir);

			//Rotate Transform towards looking
			float rotateAmount = speed * owner.delta;
			owner.mTransform.rotation = Quaternion.Slerp(owner.mTransform.rotation, targetRotation, rotateAmount);
		}
	}
}
