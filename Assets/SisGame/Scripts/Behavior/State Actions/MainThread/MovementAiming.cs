using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu(menuName = "Actions/State Actions/Movement Aiming")]
	public class MovementAiming : StateActions
	{
		public float movementSpeed = 2;
		public float movementDrag = 4;
		public float crouchSpeed = 2;

		public override void Execute(StateManager stateManager)
		{
			float moveAmount = stateManager.movementValues.moveAmount;
			Vector3 moveDirection = stateManager.movementValues.moveDirection;
			float targetSpeed = (stateManager.isCrouching ? crouchSpeed : movementSpeed);

			if (moveAmount > 0.1f)
				stateManager.rigid.drag = 0;
			else
				stateManager.rigid.drag = movementDrag;



			Vector3 velocity = moveDirection * (moveAmount * targetSpeed);
			stateManager.rigid.velocity = velocity;


		}
	}
}
