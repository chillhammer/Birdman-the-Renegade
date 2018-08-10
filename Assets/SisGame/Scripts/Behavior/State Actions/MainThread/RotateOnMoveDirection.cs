using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

namespace SA
{
	// Moves based off MoveDirection
	[CreateAssetMenu(menuName = "Actions/State Actions/Rotate On Move Direction")]
	public class RotateOnMoveDirection : StateActions
	{
		public float speed = 8;

		public override void Execute(StateManager stateManager)
		{

			Vector3 targetDir = stateManager.movementValues.moveDirection;

			if (targetDir == Vector3.zero)
				targetDir = stateManager.mTransform.forward;

			Quaternion tr = Quaternion.LookRotation(targetDir);
			float moveAmount = stateManager.movementValues.moveAmount;
			float rotateAmount = moveAmount * speed * stateManager.delta;
			stateManager.mTransform.rotation = Quaternion.Slerp(stateManager.mTransform.rotation, tr, rotateAmount);
		}
	}
}
