using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SIS
{
	public class GameController : MonoBehaviour
	{
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.F12))
			{
				SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
			}
		}

	}
}