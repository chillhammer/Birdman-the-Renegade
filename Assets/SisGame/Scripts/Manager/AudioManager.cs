using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

using SIS.Items;

namespace SIS.Managers
{
	[CreateAssetMenu(menuName = "Singles/Audio Manager")]
	public class AudioManager : ScriptableObject
	{
		[SerializeField] AudioMixerGroup soundGroup;
		public AudioMixerGroup SoundGroup { get { return soundGroup; } }

		[SerializeField] AudioMixerGroup musicGroup;
		public AudioMixerGroup MusicGroup { get { return musicGroup; } }

		[SerializeField] float soundVolume;
		public float SoundVolume { get { return soundVolume; }
			set { soundVolume = Mathf.Clamp01(value); soundGroup.audioMixer.SetFloat("SoundVol", ToVol(SoundVolume)); } }

		[SerializeField] float musicVolume;
		public float MusicVolume { get { return musicVolume; }
			set { musicVolume = Mathf.Clamp01(value); soundGroup.audioMixer.SetFloat("MusicVol", ToVol(musicVolume)); } }

		public void Init()
		{
		}

		//Converts 0-1 value to deicibel volume
		float ToVol(float nor)
		{
			if (nor >= 0.5f)
			{
				return (nor - 0.5f) / 0.5f * 20;
			}
			else
			{
				return (1 - (nor / 0.5f)) * (1 - (nor / 0.5f)) * (1 - (nor / 0.5f)) * -80;
			}
		}
	}
}