using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using System.Collections.Generic;

namespace SIS.Characters.Robo
{
	public class Vision : MonoBehaviour
	{

		public float maxDistance = 10f;
		[Range(0, 360)]
		public float angleFOV = 120f;

		[Range(5, 100)]
		public int meshResolution = 50;

		public LayerMask obstacleMask;
		public LayerMask targetMask;

		public Transform headTransform;
		public string viewMeshFilterName = "VisionMesh";
		MeshFilter viewMeshFilter;
		Mesh viewMesh;
		MeshRenderer viewMeshRenderer;

		public float MeshAlpha
		{
			get
			{
				if (viewMeshRenderer == null)
				{
					Debug.Log("viewMeshRenderer is null");
					return 0;
				}
				
				return viewMeshRenderer.material.color.a;
			}
			set
			{
				if (viewMeshRenderer == null)
				{
					Debug.Log("viewMeshRenderer is null");
					return;
				}
				Color color = viewMeshRenderer.material.color;
				Color newColor = new Color(color.r, color.g, color.b, value);
				viewMeshRenderer.material.color = newColor;
			}
		}

		[Range(0, 100)]
		public int edgeResolveIterations = 1;
		[Range(0f, 2f)]
		public float edgeDistThreshold = 0.5f;

		private void Start()
		{
			viewMesh = new Mesh();
			Transform viewMeshTransform = transform.FindDeepChild(viewMeshFilterName);
			if (viewMeshTransform == null)
			{
				Debug.LogWarning("Cannot find " + viewMeshFilterName);
			}
			viewMeshFilter = viewMeshTransform.GetComponent<MeshFilter>();
			viewMeshFilter.mesh = viewMesh;
			viewMeshRenderer = viewMeshTransform.GetComponent<MeshRenderer>();
		}

		public void LateUpdate()
		{
			//Drawing FOV
			if (MeshAlpha != 0)
				DrawFieldOfView(headTransform);
		}

		private void DrawFieldOfView(Transform transform)
		{
			float startAngle = -0.5f * angleFOV + transform.eulerAngles.y;
			float stepAngle = angleFOV / meshResolution;
			List<Vector3> viewPoints = new List<Vector3>();
			ViewCastInfo oldViewCast = new ViewCastInfo();
			for (int i = 0; i <= meshResolution; ++i)
			{
				float angle = startAngle + stepAngle * i;
				Vector3 line = DirFromAngle(angle, transform) * maxDistance;
				Vector3 start = transform.position;
				//Debug.DrawLine(start, start + line, Color.magenta);
				ViewCastInfo viewCast = ViewCast(angle, transform);

				//Add Intermediate ViewPoints Between Regular Casts
				if (i > 0)
				{
					bool diffHits = oldViewCast.hit != viewCast.hit;
					bool edgeDistExceeded = Mathf.Abs(oldViewCast.dst - viewCast.dst) > edgeDistThreshold;
					bool bothHit = oldViewCast.hit && viewCast.hit;
					if (diffHits || (bothHit && edgeDistExceeded))
					{
						EdgeInfo edge = FindEdge(oldViewCast, viewCast, transform);
						if (edge.pointA != Vector3.zero)
							viewPoints.Add(edge.pointA);

						if (edge.pointB != Vector3.zero)
							viewPoints.Add(edge.pointB);
					}
				}

				viewPoints.Add(viewCast.point);
				oldViewCast = viewCast;
			}

			if (viewPoints.Count < 1) return;

			Vector3[] vertices = new Vector3[viewPoints.Count + 1];
			int[] triangles = new int[(viewPoints.Count - 1) * 3];

			vertices[0] = Vector3.zero;
			for (int i = 0; i < viewPoints.Count - 1; ++i)
			{
				vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
				triangles[i * 3] = 0;
				triangles[i * 3 + 1] = i + 1;
				triangles[i * 3 + 2] = i + 2;
			}

			viewMesh.Clear();
			viewMesh.vertices = vertices;
			viewMesh.triangles = triangles;
			viewMesh.RecalculateNormals();
		}

		public Vector3 DirFromAngle(float angle, Transform transform, bool isGlobal = true)
		{
			if (!isGlobal) angle += transform.eulerAngles.y;
			return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
		}

		#region View Cast
		ViewCastInfo ViewCast(float globalAngle, Transform transform)
		{
			Vector3 dir = DirFromAngle(globalAngle, transform);
			RaycastHit hit;
			if (Physics.Raycast(transform.position, dir, out hit, maxDistance, obstacleMask))
			{
				return new ViewCastInfo(true, hit.point, maxDistance, globalAngle);
			}
			return new ViewCastInfo(false, transform.position + dir * maxDistance, maxDistance, globalAngle);
		}

		public struct ViewCastInfo
		{
			public bool hit;
			public Vector3 point;
			public float dst;
			public float angle;

			public ViewCastInfo(bool hit, Vector3 point, float dst, float angle)
			{
				this.hit = hit;
				this.point = point;
				this.dst = dst;
				this.angle = angle;
			}
		}

		//Only if min.hit XOR max.hit
		EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast, Transform transform)
		{
			float minAngle = minViewCast.angle;
			float maxAngle = maxViewCast.angle;
			Vector3 minPoint = Vector3.zero;
			Vector3 maxPoint = Vector3.zero;

			for (int i = 0; i < edgeResolveIterations; ++i)
			{
				float angle = (minAngle + maxAngle) * 0.5f;
				ViewCastInfo newViewCast = ViewCast(angle, transform);
				bool edgeDistExceeded = Mathf.Abs(minViewCast.dst - newViewCast.dst) > edgeDistThreshold;
				if (newViewCast.hit == minViewCast.hit && !edgeDistExceeded)
				{
					minAngle = angle;
					minPoint = newViewCast.point;
				}
				else
				{
					maxAngle = angle;
					maxPoint = newViewCast.point;
				}
			}
			return new EdgeInfo(minPoint, maxPoint);
		}

		public struct EdgeInfo
		{
			public Vector3 pointA;
			public Vector3 pointB;

			public EdgeInfo(Vector3 pointA, Vector3 pointB)
			{
				this.pointA = pointA;
				this.pointB = pointB;
			}
		}
		#endregion
	}

}