using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Actions;

namespace SIS.Camera
{
	[CreateAssetMenu(menuName = "Actions/Mono Actions/Camera/Shift Pivot")]
	public class ShiftPivot : Action
	{
		public SO.TransformVariable pivotTransform;
		public LayerMask wallLayer;
		[Range(0, 1)]
		public float pivotSpeed = 0.9f;
		[Range(0,1)]
		public float whiskerLength = 0.5f;

		[SerializeField] Vector3 defaultLocalPivotPos;

		Vector3 currentPos;
		Vector3 targetPos;

		Vector3 currentLocalPos;
		Vector3 currentLocalPosPrevious;

		private void OnEnable()
		{
			defaultLocalPivotPos = pivotTransform.value.localPosition;
			currentPos = pivotTransform.value.position;
		}

		public override void Execute()
		{
			//Default
			pivotTransform.value.localPosition = Vector3.zero;
			Vector3 pivotPos = pivotTransform.value.TransformPoint(defaultLocalPivotPos);
			targetPos = pivotPos;

			//Center
			Vector3 localCenterPivotPos = Vector3.Scale(defaultLocalPivotPos, new Vector3(0, 1, 1));
			Vector3 centerPivotPos = pivotTransform.value.TransformPoint(localCenterPivotPos);

			Vector3 dir = pivotPos - centerPivotPos;
			float dist = dir.magnitude;
			Ray ray = new Ray(centerPivotPos, dir);

			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, dist * (1 + whiskerLength), wallLayer))
			{
				targetPos = raycastHit.point - dir * whiskerLength;
			}
			//Lerping
			currentPos = Vector3.Lerp(currentPos, targetPos, pivotSpeed);
			pivotTransform.value.position = currentPos;

			currentLocalPosPrevious = pivotTransform.value.localPosition;
		}
	}
}