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
		Transform weaponTip;

		public bool isShooting;


		public void Init(Transform tip)
		{
			CreateAudioHolder();

			particles = GetComponents<ParticleSystem>();
			weaponTip = tip;
		}

		public void Shoot(Vector3 dir)
		{
			//weaponTip.rotation = Quaternion.LookRotation(dir); Updating in HandleShooting
			isShooting = true;


			if (particles != null)
			{
				
				foreach (ParticleSystem system in particles)
				{
					Debug.Log("Weapon FX Particle Effect Shot: " + system.name);
					system.Play();
				}
			}
			//StartCoroutine(ShootParticleDelay(dir));
		}

		IEnumerator ShootParticleDelay(Vector3 dir)
		{
			yield return new WaitForFixedUpdate();
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