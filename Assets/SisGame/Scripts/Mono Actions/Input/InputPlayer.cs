using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Actions.Input
{
	//Passes Inputs to Player
    [CreateAssetMenu(menuName = "Singles/Player Input Manager")]
    public class InputPlayer : Action
    {
        public InputAxis horizontal;
        public InputAxis vertical;

		public InputButton aimingButton;
		public InputButton shootButton;
		public InputButton crouchButton;
		public InputButton reloadButton;

		public InputAxis switchWeaponsAxis;
		public InputButton switchWeaponsButton;

		public float moveAmount;
		public Vector3 moveDirection;

		public SO.TransformVariable cameraTransform;
		public SO.TransformVariable pivotTransform;
		public Characters.Sis.SisVariable playerState;
		public SO.BoolVariable isPaused;
		public SO.BoolVariable inGame;


		public override void Execute()
        {
			if (isPaused.value || !inGame.value)
			{
				playerState.value.movementValues.horizontal = 0;
				playerState.value.movementValues.vertical = 0;
				playerState.value.movementValues.moveAmount = 0;
				return;
			}
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

				playerState.value.movementValues.lookDirection =
					Vector3.ProjectOnPlane(cameraTransform.value.forward, Vector3.up).normalized;
				Ray ray = new Ray(pivotTransform.value.position, pivotTransform.value.forward);
				playerState.value.movementValues.aimPosition = ray.GetPoint(100);

				if (shootButton.isPressed)
					playerState.value.isShooting = true;
				/*
				//Crouch
				if (crouchButton.isPressed)
				{
					playerState.value.ToggleCrouching();
					crouchButton.targetBoolVariable.value = playerState.value.isCrouching;
				}*/
				//Reload
				if (reloadButton.isPressed)
				{
					playerState.value.SetReloading();
					
				}

				playerState.value.switchWeaponAxis = switchWeaponsAxis.value;
				if (switchWeaponsButton.isPressed)
				{
					playerState.value.switchWeaponAxis = 1;


				}
				//reloadButton.targetBoolVariable.value = playerState.value.isReloading;



			}
        }
    }
}
