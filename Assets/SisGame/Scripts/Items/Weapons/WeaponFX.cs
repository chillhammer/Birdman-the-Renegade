using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Items.Weapons
{
	//Visual/Auditory Effects For Weapon
	public class WeaponFX : MonoBehaviour
	{
		ParticleSystem[] particles;
		AudioSource audioSource;

		public bool isShooting;


		public void Init()
		{
			CreateAudioHolder();

			particles = GetComponentsInChildren<ParticleSystem>();
		}

		public void Shoot()
		{
			isShooting = true;

			if (particles != null)
			{
				foreach (ParticleSystem system in particles)
				{
					system.Play();
				}
			}
		}

		//Helper Functions
		protected void CreateAudioHolder()
		{
			GameObject go = new GameObject();
			go.name = "Audio Holder";
			go.transform.parent = this.transform;
			audioSource = go.AddComponent<AudioSource>();
			audioSource.spatialBlend = 1;
		}
	}
}