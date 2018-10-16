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

		float timer = 0;

		public override void Execute(RoboPadron owner)
		{
			if (owner.vision == null)
				return;
			//Update Timer
			if (timer < frequencyInSeconds) {
				timer += owner.delta;
				return;
			}
			timer = 0;
			if (playerTransform.value != null) {
				Vector3 playerPos = playerTransform.value.position;
				Vector3 headAngle = owner.headBone.forward;
				Vector3 headPos = owner.headBone.position;
				float playerDist = Vector3.Distance(playerPos, owner.mTransform.position);
				if (playerDist <= owner.vision.maxDistance)
				{
					//Debug.Log("Within Dist!");
					Vector3 dir = (playerPos - headPos).normalized;
					if (Vector3.Angle(headAngle, dir) < owner.vision.angleFOV *0.5f)
					{
						//Debug.Log("Within Angle! Dir: " + dir + " - HeadAngle: " + headAngle);
						RaycastHit raycastHit;
						LayerMask mapAndPlayer = LayerMask.NameToLayer("Player") | LayerMask.NameToLayer("Map");
						Ray ray = new Ray(headPos, dir);

						Debug.DrawLine(headPos, playerPos, Color.blue, 0.25f);
						if (Physics.Raycast(ray, out raycastHit, owner.vision.maxDistance, ~mapAndPlayer))
						{
							//Debug.Log("Hit Something. Tag is " + raycastHit.collider.tag + ". Name is " + raycastHit.collider.name);
							if (raycastHit.collider.tag == "Player")
							{
								if (!owner.canSeePlayer)
								{
									if (onPlayerSightEvent != null)
										onPlayerSightEvent.Raise();

									//Update Last Known Location
									owner.playerLastKnownLocation.position = playerPos;
									owner.playerLastKnownLocation.timeSeen = Time.realtimeSinceStartup;
								}
								owner.canSeePlayer = true;
							} else
							{
								owner.canSeePlayer = false;
							}
						}
					}
				}
			}
		}
	}
}