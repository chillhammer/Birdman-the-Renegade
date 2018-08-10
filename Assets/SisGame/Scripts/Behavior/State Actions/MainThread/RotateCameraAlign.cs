using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;

namespace SA
{
	// Moves to align with camera
	[CreateAssetMenu(menuName = "Actions/State Actions/Rotate Align with Camera")]
	public class RotateCameraAlign : StateActions
	{
		public TransformVariable cameraTransform;
		public float speed = 8;

		public override void Execute(StateManager stateManager)
		{
			Vector3 targetDir = stateManager.movementValues.lookDirection;

			if (targetDir == Vector3.zero)
				targetDir = stateManager.mTransform.forward;

			Quaternion tr = Quaternion.LookRotation(targetDir);
			float rotateAmount = speed * stateManager.delta;
			stateManager.mTransform.rotation = Quaternion.Slerp(stateManager.mTransform.rotation, tr, rotateAmount);
		}
	}
}
