using UnityEngine;
using System.Collections;

namespace SIS.Actions.Input
{
	[CreateAssetMenu(menuName = "Actions/Mono Actions/Rotate Via Input")]
	public class RotateViaInput : Action
	{
		public InputAxis targetInput;
		public SO.TransformVariable targetTransform;

		public float angle;
		public float speed;
		public bool negative;
		public bool clamp;
		public float minClamp = -35;
		public float maxClamp = 35;
		public RotateAxis targetAxis;
		public SO.FloatVariable sensitivity;
		public SO.BoolVariable inGame;

		public enum RotateAxis { x, y, z }

		public override void Execute()
		{
			if (inGame.value == false)
				return;
			//Modify Angle based on input
			int neg = (negative ? -1 : 1);

			angle += targetInput.value * speed * Time.deltaTime * neg * sensitivity.value;
			if (clamp)
				angle = Mathf.Clamp(angle, minClamp, maxClamp);
			
			//Set to new Rotation
			Vector3 newRotation = targetTransform.value.localRotation.eulerAngles;
			switch (targetAxis)
			{
				case RotateAxis.x:
					newRotation.x = angle;
					break;
				case RotateAxis.y:
					newRotation.y = angle;
					break;
				case RotateAxis.z:
					newRotation.z = angle;
					break;
			}
			targetTransform.value.localRotation = Quaternion.Euler(newRotation);
			
		}
	}
}
