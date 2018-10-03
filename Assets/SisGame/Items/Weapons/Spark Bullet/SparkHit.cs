using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows for particle system to be retrieved
public class SparkHit : MonoBehaviour {

	ParticleSystem particleSystem;

	private void Start()
	{
		particleSystem = GetComponent<ParticleSystem>();
	}
}
