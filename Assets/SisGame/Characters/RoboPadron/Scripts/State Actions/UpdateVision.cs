using UnityEngine;
using UnityEditor;
using SIS.States.Actions;

namespace SIS.Characters.Robo
{
	//What can RoboPadron see?
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Update Vision")]
	public class UpdateVision : RoboPadronStateActions
	{
		public float frequencyInSeconds = 0.25f;
		public SO.TransformVariable playerTransform;
		public SO.GameEvent onPlayerSightEvent;
		public float lightSwitchSpeed = 5f;

		//float timer = 0;
		float originalMeshAlpha = -1;

		public override void Execute(RoboPadron owner)
		{
			if (owner.vision == null)
				return;
			//Mesh Opacity
			UpdateMeshAlpha(owner);

			//Update Timer
			if (owner.visionTimer < frequencyInSeconds) {
				owner.visionTimer += owner.delta;
				return;
			}
			if (owner.IsDead())
				return;

			/*
			if (owner.vision.MeshAlpha == 0)
				return;
			*/
			owner.visionTimer = 0;
			if (playerTransform.value != null) {
				Vector3 playerPos = playerTransform.value.position;
				Vector3 headAngle = owner.headBone.forward;
				Vector3 headPos = owner.headBone.position;
				float playerDist = Vector3.Distance(playerPos, owner.mTransform.position);
				if (playerDist <= owner.vision.maxDistance)
				{
					//Debug.Log("Within Dist!");
					Vector3 dir = (playerPos - headPos).normalized;
					Debug.DrawLine(headPos, headPos + dir * 100, Color.cyan, 0.25f);
					//if (Vector3.Angle(headAngle, dir) < owner.vision.angleFOV *0.5f)
					if (Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(headAngle, dir)) < owner.vision.angleFOV * 0.5f)
					{
						Debug.DrawLine(headPos, playerPos, Color.green, 0.25f);
						//Debug.Log("Within Angle! Dir: " + dir + " - HeadAngle: " + headAngle);
						RaycastHit raycastHit;
						LayerMask mapAndPlayer = LayerMask.NameToLayer("Player") | LayerMask.NameToLayer("Map");
						Ray ray = new Ray(headPos, dir);

						if (Physics.Raycast(ray, out raycastHit, owner.vision.maxDistance, ~mapAndPlayer))
						{
							//Debug.Log("Hit Something. Tag is " + raycastHit.collider.tag + ". Name is " + raycastHit.collider.name);
							if (raycastHit.collider.tag == "Player")
							{
								Debug.DrawLine(headPos, playerPos, Color.blue, 0.25f);
								if (!owner.canSeePlayer)
								{
									if (onPlayerSightEvent != null)
										onPlayerSightEvent.Raise();
								}
								//Update Last Known Location
								owner.playerLastKnownLocation.position = playerPos;
								owner.playerLastKnownLocation.timeSeen = Time.realtimeSinceStartup;

								owner.canSeePlayer = true;
							} else
							{
								owner.canSeePlayer = false;
							}
						}
					} else
					{
						//Debug.Log("Not Within Angle! Dir: " + dir + " - HeadAngle: " + headAngle);
					}
				}
			}
		}

		void UpdateMeshAlpha(RoboPadron owner)
		{
			if (originalMeshAlpha == -1f)
			{
				originalMeshAlpha = owner.vision.MeshAlpha;
			}

			float targetAlpha;
			if (owner.canSeePlayer || owner.health == 0)
			{
				targetAlpha = 0;
			} else
			{
				targetAlpha = originalMeshAlpha;
			}
			owner.vision.MeshAlpha = Mathf.Lerp(owner.vision.MeshAlpha, targetAlpha, owner.delta * lightSwitchSpeed);
		}
	}
}