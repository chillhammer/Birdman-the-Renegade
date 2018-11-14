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


		public void Init(Transform tip, AudioClip sound)
		{
			CreateAudioHolder(sound);

			particles = GetComponents<ParticleSystem>();
			weaponTip = tip;
		}

		public void Shoot(Vector3 dir)
		{
			//weaponTip.rotation = Quaternion.LookRotation(dir); Updating in HandleShooting
			isShooting = true;
			audioSource.PlayOneShot(audioSource.clip);

			if (particles != null)
			{
				
				foreach (ParticleSystem system in particles)
				{
					Debug.Log("Weapon FX Particle Effect Shot: " + system.name);
					system.Play();
				}
			}
		}

		public void Reload(AudioClip reloadSound)
		{
			audioSource.PlayOneShot(reloadSound);
		}


		//Helper Functions
		protected void CreateAudioHolder(AudioClip sound)
		{
			GameObject go = new GameObject();
			go.name = "Audio Holder";
			go.transform.parent = this.transform;
			go.transform.localPosition = Vector3.zero;
			audioSource = go.AddComponent<AudioSource>();
			audioSource.spatialBlend = 0;
			audioSource.volume = 0.1f;
			audioSource.clip = sound;
			audioSource.outputAudioMixerGroup = Managers.GameManagers.AudioManager.SoundGroup;
		}
	}
}