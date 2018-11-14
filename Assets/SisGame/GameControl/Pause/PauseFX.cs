using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace SIS.GameControl
{
	//Pause is set by Game Controller. This just animates and controls step by step
	public class PauseFX : MonoBehaviour
	{
		[SerializeField] GameObject pauseParent;
		[SerializeField] GameObject blackScreen;

		[SerializeField] SO.BoolVariable isPaused;

		[SerializeField] float backgroundOpacity = 0.5f;
		[SerializeField] float backgroundSpeed = 4f;
		[SerializeField] GameObject backgroundInstance;

		[SerializeField] float scaleSpeed = 7f;

		[SerializeField] TextMeshProUGUI deathText;
		[SerializeField] SO.IntVariable deaths;

		RectTransform pauseTransform;
		float timer = 0;
		
		RawImage backgroundImage;

		float PauseScale
		{
			get { return pauseTransform.localScale.x; }
			set {
				pauseTransform.gameObject.SetActive(value > 0.01f ? true : false);
				pauseTransform.localScale = new Vector3(value, value, value);
			}
		}

		float BackgroundAlpha
		{
			get { if (!backgroundImage.IsActive()) { return 0; }; return backgroundImage.color.a; }
			set {
				Color color = backgroundImage.color;
				color.a = value;
				backgroundImage.color = color;
				backgroundInstance.SetActive(value > 0.05f ? true : false);
			}
		}

		private void Start()
		{
			pauseTransform = pauseParent.GetComponent<RectTransform>();
			if (backgroundInstance != null)
				backgroundImage = backgroundInstance.GetComponent<RawImage>();
		}

		private void Update()
		{
			bool paused = isPaused.value;
			PauseScale = Mathf.Lerp(PauseScale, paused ? 1 : 0, scaleSpeed * Time.unscaledDeltaTime);
			if (PauseScale < 0.01f)
				PauseScale = 0;
			if (paused)
			{
				deathText.text = "Deaths: " + deaths.value;
				if (backgroundInstance == null)
				{
					backgroundInstance = Instantiate(blackScreen);
					RectTransform trans = backgroundInstance.GetComponent<RectTransform>();
					trans.parent = transform;
					
					trans.localPosition = Vector3.zero;

					backgroundImage = backgroundInstance.GetComponent<RawImage>();
				}
			}
			if (backgroundInstance != null)
				BackgroundAlpha = Mathf.Lerp(BackgroundAlpha, (paused ? backgroundOpacity : 0), backgroundSpeed * Time.unscaledDeltaTime);
		}
	}
}