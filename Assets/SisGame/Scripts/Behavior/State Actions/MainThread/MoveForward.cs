using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu(menuName = "Actions/State Actions/Move Forward")]
	public class MoveForward : StateActions
	{
		public float movementSpeed = 2;
		public float movementDrag = 4;
		public float crouchSpeed = 2;

		public override void Execute(StateManager stateManager)
		{
			float moveAmount = stateManager.movementValues.moveAmount;
			float targetSpeed = (stateManager.isCrouching ? crouchSpeed : movementSpeed);

			if (moveAmount > 0.1f)
				stateManager.rigid.drag = 0;
			else
				stateManager.rigid.drag = movementDrag;



			Vector3 velocity = stateManager.mTransform.forward * moveAmount * targetSpeed;
			stateManager.rigid.velocity = velocity;


		}
	}
}
