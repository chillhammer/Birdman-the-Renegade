using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SIS.Menu
{
	public class ChangeSceneButtonFunctionality : MonoBehaviour
	{
		public string sceneName;
		public UnityEngine.SceneManagement.Scene scene;

		public float timeFade = 0.02f;
		public GameObject blackScreen;
		UnityEngine.UI.RawImage blackScreenImage;
		float timer = -1;
		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.C))
			{
				//Instantiate(blackScreen, Vector3.zero, Quaternion.identity, transform.parent);
			}
			if (timer >= 0)
			{
				timer += Time.deltaTime;
				Color color = blackScreenImage.color;
				color.a = Mathf.Min(timer / timeFade, 1);
				blackScreenImage.color = color;
			}
		}

		public void GoToScene()
		{
			Debug.Log("GoToScene() called.");
			//UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
			UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
			timer = 0;
			blackScreenImage = Instantiate(blackScreen, Vector3.zero, Quaternion.identity, transform.parent).GetComponent<RawImage>();
			blackScreenImage.rectTransform.localPosition = Vector3.zero;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}