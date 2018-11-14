using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Waypoints;
namespace SIS.Characters.Bill
{
    public class Bill : MonoBehaviour, IHittable
    {
        [SerializeField]
        SO.TransformVariable playerTransform;
        [SerializeField]
        LayerMask visionMask;
        [SerializeField]
        AudioSource audioSource;
        [SerializeField]
        AudioClip angrySound;
		[SerializeField]
		AudioClip deathSound;
		[SerializeField]
        Sis.SisVariable sisVariable;
        [SerializeField]
        AnimationCurve deathSizeCurve;
		[SerializeField]
		ParticleSystem bubbleParticleSystem;
		[SerializeField]
		ParticleSystem sparkParticleSystem;
		[SerializeField]
		SO.FloatVariable angrySpeedVariable;
		WaypointNavigator waypointNavigator;
        CharacterController charactercontroller;

        private float timer = 0f;
        public bool dead = false;
        [SerializeField]
        private bool angry = false;
        public float normalspeed = 1;
        private float angryspeed = 4;
		public float rotationspeed = 3;
        public float hitDistance = 1f;
        // Use this for initialization
        void Start()
        {
            waypointNavigator = GetComponent<WaypointNavigator>();
            waypointNavigator.StartNavigation(playerTransform.value.position);
            charactercontroller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
		{
			angryspeed = angrySpeedVariable.value;

			UpdateBubbleParticleSystem();

			if (dead)
			{
				timer += Time.deltaTime;

				float deathTime = 0.5f;
				float deathSize = deathSizeCurve.Evaluate(timer / deathTime);
				transform.localScale = new Vector3(deathSize, deathSize, deathSize);

				//Debug.Log("DeathSize: " + deathSize);

				if (timer >= deathTime)
				{
					//Bubbles
					bubbleParticleSystem.gameObject.transform.parent = transform.parent;
					Destroy(bubbleParticleSystem.gameObject, 1f);
					bubbleParticleSystem.Emit(3);

					//Sparks
					sparkParticleSystem.gameObject.transform.parent = transform.parent;
					Destroy(sparkParticleSystem.gameObject, 1f);
					sparkParticleSystem.Play();


					//Damage
					if (playerTransform.value != null)
					{
						if (Vector3.Distance(transform.position, playerTransform.value.position) < hitDistance + 1f)
						{
							Vector3 force = (playerTransform.value.position - transform.position).normalized * 10;
							sisVariable.value.OnHit(null, 5f, force, transform.position);
							//Debug.Log("Bill hit sis!");
						}
					}

					Destroy(gameObject);
				}
				return;
			}
			if (playerTransform.value != null)
			{
				//If There is a Straight Path
				if (angry && CanSeePlayer())
				{
					//Naive Pathfinding
					waypointNavigator.StopNavigate();
					Vector3 dirtoplayer = (playerTransform.value.position - transform.position).normalized;
					charactercontroller.Move(angryspeed * dirtoplayer * Time.deltaTime);
					dirtoplayer.y = 0;
					Quaternion quatdir = Quaternion.LookRotation(dirtoplayer);
					float rotationspeed = 5f;
					transform.rotation =
						Quaternion.Slerp(transform.rotation, quatdir, rotationspeed * Time.deltaTime);
				}
				else
				{
					//Smart pathfinding
					timer += Time.deltaTime;
					if (timer > 0.25f)
					{
						timer = 0;
						if (waypointNavigator.PathFinished)
							waypointNavigator.StartNavigation(playerTransform.value.position);
						// First time seeing the player.
						if (angry == false)
						{
							if (CanSeePlayer())
							{
								angry = true;
								audioSource.PlayOneShot(angrySound, 0.1f);
								bubbleParticleSystem.Emit(5);
								//Debug.Log("Bill saw the player");

							}
						}
					}
				}

			}

			// Movement and rotation.
			if (!waypointNavigator.PathFinished)
			{
				float speed = (angry ? angryspeed : normalspeed);
				Vector3 dir = waypointNavigator.DirectionToWaypoint;
				charactercontroller.Move(transform.forward * speed * Time.deltaTime);
				dir.y = 0;
				Quaternion quatdir = Quaternion.LookRotation(dir);
				transform.rotation =
					Quaternion.Slerp(transform.rotation, quatdir, rotationspeed * Time.deltaTime);
			}
			//Hit Sis!
			if (Vector3.Distance(transform.position, playerTransform.value.position) < hitDistance)
			{
				//Vector3 force = (playerTransform.value.position - transform.position).normalized * 25;

				//sisVariable.value.OnHit(null, 5f, force, transform.position);
				SetDead();

			}

		}

		public bool IsDead()
		{
			return dead;

		}
		public void PlaySound(AudioClip audio)
		{
			audioSource.PlayOneShot(audio, 0.5f);
		}

		//Helpers

		private void UpdateBubbleParticleSystem()
		{
			//Update Bubble ParticleSystem
			if (!angry)
			{
				SetBubbles(1, normalspeed);
			}
			else if (angry)
			{
				SetBubbles(2, angryspeed * 5.5f);
			}
		}

		bool CanSeePlayer()
        {
            Vector3 dirtoplayer = playerTransform.value.position - transform.position;
            Ray ray = new Ray(transform.position, dirtoplayer);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, 100f, visionMask))
            {
                //Debug.Log(raycastHit.collider.name);
                if (raycastHit.collider.tag == "Player")
                    return true;
            }
            return false;

        }

        public void OnHit(Character shooter, float baseDamage, Vector3 dir, Vector3 pos)
        {
            //To do: Create Explosion, death animation.
            SetDead();
        }

		public int GetScore()
		{
			return 10;
		}

		void SetDead()
		{
			if (!dead)
			{
				dead = true;
				timer = 0;
				audioSource.PlayOneShot(deathSound, 1f);
			}
        }

        

        public void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, hitDistance);
        }

		private void SetBubbles(float speedMultiplier, float rate)
		{
			ParticleSystem.VelocityOverLifetimeModule velocity = bubbleParticleSystem.velocityOverLifetime;
			velocity.speedModifierMultiplier = speedMultiplier;
			ParticleSystem.EmissionModule emission = bubbleParticleSystem.emission;
			emission.rateOverTime = normalspeed;

			
		}
    }
}