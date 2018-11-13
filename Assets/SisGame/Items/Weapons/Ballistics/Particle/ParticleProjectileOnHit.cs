using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Collision Events for Particle Bullet
//Is added on by ParticleBallistics
// Allows for particle system to be retrieved
namespace SIS.Items.Weapons
{
	public class ParticleProjectileOnHit : MonoBehaviour
	{

		ParticleSystem part;
		ParticleSystem onHitParticleSystem;
		public List<ParticleCollisionEvent> collisionEvents;

		Characters.Character owner;
		Weapon weapon;
		Vector3 incomingDirection;
		float baseDamage;
		float justHit = 0;

		private void Start()
		{
			part = GetComponent<ParticleSystem>();
			//On Hit Particle System is Second On List
			ParticleSystem[] ps = transform.parent.GetComponentsInChildren<ParticleSystem>();
			if (ps != null && ps.Length > 1)
				onHitParticleSystem = ps[1];

			collisionEvents = new List<ParticleCollisionEvent>();
			justHit = 0;
		}

		private void Update()
		{
			justHit = Mathf.Max(0, justHit - 1);
		}

		public void UpdateOnHitSettings(Characters.Character owner, Vector3 dir, LayerMask ignore, float baseDamage)
		{
			this.owner = owner;
			this.baseDamage = baseDamage;
			incomingDirection = dir;
			ParticleSystem.CollisionModule coll = part.collision;
			coll.collidesWith = ~ignore;
		}

		void OnParticleCollision(GameObject other)
		{
			if (owner == null)
				return;
			int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
			//Debug.Log("Particle Hit " + other.name + ". Times: " + numCollisionEvents);
			if (numCollisionEvents > 0)
			{
				incomingDirection = collisionEvents[0].velocity.normalized;
				Vector3 intersection = collisionEvents[0].intersection;
				if (onHitParticleSystem != null)
				{
					onHitParticleSystem.transform.position = intersection;
				}

				IHittable isHittable = other.GetComponentInParent<IHittable>();
				if (isHittable == null)
				{
					//Not Enemy
					ParticleSystem.MainModule mainModule = onHitParticleSystem.main;
					mainModule.startLifetime = 0.03f;
				}
				else
				{
					//You hit enemy!
					if (onHitParticleSystem != null)
						onHitParticleSystem.transform.position = intersection + incomingDirection * 0.3f;
					ParticleSystem.MainModule mainModule = onHitParticleSystem.main;
					mainModule.startLifetime = 0.05f;

					isHittable.OnHit(owner, baseDamage, incomingDirection, intersection);
				}
				if (onHitParticleSystem != null)
					onHitParticleSystem.Play();

			}
		}
	}
}