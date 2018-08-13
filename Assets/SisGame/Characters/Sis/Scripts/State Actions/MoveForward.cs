using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.States;

namespace SIS.Characters.Sis
{
	//Handles Movement based on object transform direction
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Move Forward")]
	public class MoveForward : SisStateActions
	{
		public float movementSpeed = 2;
		public float movementDrag = 4;
		public float crouchSpeed = 2;

		public override void Execute(Sis owner)
		{
			//Handles Move Amount
			float moveAmount = owner.movementValues.moveAmount;
			float targetSpeed = (owner.isCrouching ? crouchSpeed : movementSpeed);

			if (moveAmount > 0.1f)
				owner.rigid.drag = 0;
			else
				owner.rigid.drag = movementDrag;


			//Set Velocity
			Vector3 velocity = owner.mTransform.forward * moveAmount * targetSpeed;
			owner.rigid.velocity = velocity;


		}
	}
}
