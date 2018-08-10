using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Actions
{
	[CreateAssetMenu(menuName = "Actions/Mono Actions/Handle Local Position On Axis")]
	public class HandleLocalPositionAxis : Action
	{
		public SO.TransformVariable targetTransform;

		public SO.BoolVariable targetBool;
		public float defaultValue; //use original
		public float affectedValue;

		public float speed = 9;
		public Axis targetAxis;

		
		private void OnEnable()
		{
			if (targetTransform.value == null)
				return;
			defaultValue = GetCurrentPositionOnAxis(targetAxis);
		}
		
		public override void Execute()
		{
			if (targetTransform.value == null)
				return;

			//Target Based On Input
			float targetValue = (targetBool.value ? affectedValue : defaultValue);

			float currentValue = GetCurrentPositionOnAxis(targetAxis);

			//Lerp Towards Target
			float newValue = Mathf.Lerp(currentValue, targetValue, speed * Time.deltaTime);

			Vector3 newPosition = targetTransform.value.localPosition;
			switch (targetAxis)
			{
				case Axis.x:
					newPosition.x = newValue;
					break;
				case Axis.y:
					newPosition.y = newValue;
					break;
				case Axis.z:
					newPosition.z = newValue;
					break;
				default:
					break;
			}
			targetTransform.value.localPosition = newPosition;
		}

		public enum Axis
		{
			x, y, z
		}

		float GetCurrentPositionOnAxis(Axis axis)
		{
			switch (axis)
			{
				case Axis.x:
					return targetTransform.value.localPosition.x;
				case Axis.y:
					return targetTransform.value.localPosition.y;
				case Axis.z:
					return targetTransform.value.localPosition.z;
			}
			return 0;
		}
	}
}
