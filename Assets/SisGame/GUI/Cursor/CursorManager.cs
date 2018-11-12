using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Actions.Input;
using System;

namespace SIS.Managers
{
	[DisallowMultipleComponent]
	public class CursorManager : MonoBehaviour
	{
		[SerializeField] private InputAxis mouseX;
		[SerializeField] private InputAxis mouseY;
		[SerializeField] private Characters.Sis.SisVariable player;
		[SerializeField] private SO.FloatVariable crosshairSpreadVariable;
		[SerializeField] private SO.BoolVariable isAiming;

		[SerializeField] CrosshairPart[] crosshairParts;
		[SerializeField] private float spreadSpeed = 3;
		[SerializeField] private float spreadMaxDist = 50;
		[SerializeField] private float spreadMinDist = 10;
		[Range(0,100)]
		[SerializeField] private float mouseEmphasis = 0.5f;
		[Range(0, 100)]
		[SerializeField] private float movementEmphasis = 0.5f;
		[Range(0, 1)]
		[SerializeField] private float aimingEmphasisMultiplier;
		[SerializeField] private SO.BoolVariable isPaused;
		[SerializeField] private SO.BoolVariable inGame;


		float currentSpread;


		// Use this for initialization
		void Start()
		{
			//Look in Game Control for cursor management (pause)
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}

		// Update is called once per frame
		void Update()
		{
			if (!inGame.value)
				return;
			HandleCrosshairSpread();

			//Update Scriptable CrosshairSpread for Shooting
			crosshairSpreadVariable.value = currentSpread;

			
		}

		/// <summary>
		/// Allows for crosshairs to move based on movement
		/// </summary>
		private void HandleCrosshairSpread()
		{
			if (crosshairParts == null)
				return;
			float targetSpread = CalculateSpread();

			//Limits spread if aiming
			float aimingMult = 1;
			if (isAiming.value) aimingMult = 1/ aimingEmphasisMultiplier;

			currentSpread = Mathf.Lerp(currentSpread, targetSpread, spreadSpeed * aimingMult * Time.deltaTime);

			foreach (CrosshairPart crosshair in crosshairParts)
			{
				crosshair.transform.anchoredPosition = crosshair.direction * currentSpread;
			}
		}

		//Formula to find spread, use mouse and player movement
		private float CalculateSpread()
		{
			float mouseMovement = Mathf.Abs(mouseX.value) + Mathf.Abs(mouseY.value);
			float movement = 0;
			if (player != null)
				movement = player.value.movementValues.moveAmount;

			float rawSpread = mouseMovement * mouseEmphasis + movement * movementEmphasis;

			//Limits spread if aiming
			float aimingMult = 1;
			if (isAiming.value) aimingMult = aimingEmphasisMultiplier;

			return Mathf.Clamp(rawSpread, spreadMinDist, spreadMaxDist) * aimingMult;
		}

		[System.Serializable]
		public class CrosshairPart {
			public RectTransform transform;
			public Vector2 direction;
		}
	}
}