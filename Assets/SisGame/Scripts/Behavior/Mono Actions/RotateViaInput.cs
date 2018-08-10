using UnityEngine;
using System.Collections;

namespace SA
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

		public enum RotateAxis { x, y, z }

		public override void Execute()
		{
			if (!negative)
				angle += targetInput.value * speed;
			else
				angle -= targetInput.value * speed;
			
			if (clamp)
			{
				angle = Mathf.Clamp(angle, minClamp, maxClamp);
			}
			
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
