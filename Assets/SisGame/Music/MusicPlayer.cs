using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SIS.Music
{
	public class MusicPlayer : MonoBehaviour
	{
		[SerializeField] AudioClip titleScreenMusic;
		[SerializeField] AudioClip battleMusic;

		AudioSource musicPlayer;

		// Use this for initialization
		void Start()
		{
			musicPlayer = GetComponent<AudioSource>();
			SceneManager.activeSceneChanged += OnSceneChanged;
			DontDestroyOnLoad(gameObject);
			OnSceneChanged(SceneManager.GetActiveScene(), SceneManager.GetActiveScene());
		}

		//Music handling when scene changes
		void OnSceneChanged(Scene scene, Scene next)
		{
			if (next.name == "Dungeon")
			{
				musicPlayer.clip = battleMusic;
			}
			else
			if (next.name == "MainMenu")
			{
				musicPlayer.clip = titleScreenMusic;
			}
			musicPlayer.Play();
		}
	}
}