using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
    [CreateAssetMenu]
    public class InputManager : Action
    {
        public InputAxis horizontal;
        public InputAxis vertical;

		public InputButton aimingButton;
		public InputButton shootButton;
		public InputButton crouchButton;
		public InputButton reloadButton;

		public float moveAmount;
		public Vector3 moveDirection;

		public SO.TransformVariable cameraTransform;
		public SO.TransformVariable pivotTransform;
		public StateMangerVariable playerState;


		public override void Execute()
        {
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal.value) + Math.Abs(vertical.value));

			if (cameraTransform.value != null)
			{
				moveDirection = cameraTransform.value.forward * vertical.value;
				moveDirection += cameraTransform.value.right * horizontal.value;
				moveDirection.y = 0;
				moveDirection.Normalize();
			}

			if (playerState.value != null)
			{
				playerState.value.movementValues.horizontal = horizontal.value;
				playerState.value.movementValues.vertical = vertical.value;
				playerState.value.movementValues.moveAmount = moveAmount;
				playerState.value.movementValues.moveDirection = moveDirection;
				playerState.value.isAiming = aimingButton.isPressed;
				playerState.value.isShooting = shootButton.isPressed;
				//Crouch
				if (crouchButton.isPressed)
				{
					playerState.value.ToggleCrouching();
					crouchButton.targetBoolVariable.value = playerState.value.isCrouching;
				}
				//Reload
				if (reloadButton.isPressed)
				{
					playerState.value.SetReloading();
					
				}
				reloadButton.targetBoolVariable.value = playerState.value.isReloading;

				playerState.value.movementValues.lookDirection =
					Vector3.ProjectOnPlane(cameraTransform.value.forward, Vector3.up).normalized;
				Ray ray = new Ray(pivotTransform.value.position, pivotTransform.value.forward);
				playerState.value.movementValues.aimPosition = ray.GetPoint(100);
				
			}
        }
    }
}
