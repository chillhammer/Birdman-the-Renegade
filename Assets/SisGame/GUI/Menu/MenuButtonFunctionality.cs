using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Menu
{
	public class MenuButtonFunctionality : MonoBehaviour
	{
		public string sceneName;
		public UnityEngine.SceneManagement.Scene scene;
		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public void GoToScene()
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
		}
	}
}