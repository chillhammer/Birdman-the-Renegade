using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Actions.Input
{
	[CreateAssetMenu(menuName = "Inputs/Button")]
	public class InputButton : Action
	{
		public string targetInput;
		public bool isPressed;
		public KeyState keyState;
		public bool updateBoolVar = true;
		public SO.BoolVariable targetBoolVariable;


		public override void Execute()
		{
			//Get Pressed State
			switch (keyState)
			{
				case KeyState.onDown:
					isPressed = UnityEngine.Input.GetButtonDown(targetInput);
					break;
				case KeyState.onCurrent:
					isPressed = UnityEngine.Input.GetButton(targetInput);
					break;
				case KeyState.onUp:
					isPressed = UnityEngine.Input.GetButtonUp(targetInput);
					break;
				default:
					break;
			}

			//Set Bool Variable
			if (updateBoolVar)
			{
				if (targetBoolVariable != null)
				{
					targetBoolVariable.value = isPressed;
				}
			}
		}

		public enum KeyState
		{
			onDown,onCurrent,onUp
		}
	}
}
