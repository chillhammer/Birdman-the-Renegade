using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Collision Events for Particle Bullet
// Allows for particle system to be retrieved
public class SparkHit : MonoBehaviour {

	ParticleSystem particleSystem;
	public List<ParticleCollisionEvent> collisionEvents;


	private void Start()
	{
		particleSystem = GetComponent<ParticleSystem>();
		collisionEvents = new List<ParticleCollisionEvent>();

	}

	void OnParticleCollision(GameObject other)
	{

		int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);

		Rigidbody rb = other.GetComponent<Rigidbody>();
		int i = 0;

		while (i < numCollisionEvents)
		{
			if (rb)
			{
				Vector3 pos = collisionEvents[i].intersection;
				Vector3 force = collisionEvents[i].velocity * 10;
				rb.AddForce(force);
			}
			i++;
		}
	}
}
