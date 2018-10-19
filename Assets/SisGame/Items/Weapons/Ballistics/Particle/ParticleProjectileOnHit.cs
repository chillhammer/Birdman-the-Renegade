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

		private void Start()
		{
			part = GetComponent<ParticleSystem>();
			//On Hit Particle System is Second On List
			ParticleSystem[] ps = transform.parent.GetComponentsInChildren<ParticleSystem>();
			if (ps != null && ps.Length > 1)
				onHitParticleSystem = ps[1];

			collisionEvents = new List<ParticleCollisionEvent>();

		}

		public void UpdateOnHitSettings(Characters.Character owner, Weapon wep, Vector3 dir, LayerMask ignore)
		{
			this.owner = owner;
			weapon = wep;
			incomingDirection = dir;
			ParticleSystem.CollisionModule coll = part.collision;
			coll.collidesWith = ~ignore;
			//Not Used, Could Be Replaced with Muzzle Flash.
			/*
			onHitParticleSystem.transform.position = 
				(weapon.runtime.weaponTip != null ? 
					weapon.runtime.weaponTip.position : 
					weapon.runtime.modelInstance.transform.position);
			*/
		}

		void OnParticleCollision(GameObject other)
		{

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
				}
				else
				{
					if (onHitParticleSystem != null)
						onHitParticleSystem.transform.position = intersection + incomingDirection * 0.3f;
					isHittable.OnHit(owner, weapon, incomingDirection, intersection);
				}
				if (onHitParticleSystem != null)
					onHitParticleSystem.Play();
			}
		}
	}
}