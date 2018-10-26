using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Actions;

namespace SIS.Camera
{
	[CreateAssetMenu(menuName = "Actions/Mono Actions/Camera/Wall Zoom Override")]
	public class WallZoomOverride : Action
	{
		public SO.TransformVariable pivotTransform;
		public SO.TransformVariable actualCameraTransform;
		public LayerMask wallLayer;

		public override void Execute()
		{
			Vector3 dir = actualCameraTransform.value.position - pivotTransform.value.position;
			float dist = dir.magnitude;
			Ray ray = new Ray(pivotTransform.value.position, dir);

			RaycastHit raycastHit;
			if (Physics.Raycast(ray, out raycastHit, dist, wallLayer))
			{
				//Debug.Log("Hitting Wall");
				/*
				Vector3 localHit = actualCameraTransform.value.InverseTransformPoint(raycastHit.point);
				Vector3 localPos = actualCameraTransform.value.localPosition;
				localPos.z = localHit.z;
				actualCameraTransform.value.localPosition = localPos;
				*/
				actualCameraTransform.value.position = raycastHit.point - dir *0.01f;
			}
		}
	}
}