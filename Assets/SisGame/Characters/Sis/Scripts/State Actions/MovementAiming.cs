using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.States;

namespace SIS.Characters.Sis
{
	//Move based on input move direction
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Movement Aiming")]
	public class MovementAiming : SisStateActions
	{
		public float movementSpeed = 2;
		public float movementDrag = 4;
		public float crouchSpeed = 2;

		public override void Execute(Sis owner)
		{
			//Handle Move Amount and Direction
			float moveAmount = owner.movementValues.moveAmount;
			Vector3 moveDirection = owner.movementValues.moveDirection;
			float targetSpeed = (owner.isCrouching ? crouchSpeed : movementSpeed);

			if (moveAmount > 0.1f)
				owner.rigid.drag = 0;
			else
				owner.rigid.drag = movementDrag;


			//Set Velocity
			Vector3 velocity = moveDirection * (moveAmount * targetSpeed);
			owner.rigid.velocity = velocity;


		}
	}
}
