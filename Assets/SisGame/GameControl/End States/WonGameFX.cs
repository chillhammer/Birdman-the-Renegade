using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SIS.GameControl
{
	//When You won the game
	public class WonGameFX : MonoBehaviour
	{

		[SerializeField] float lostSpeed = 2f;
		[SerializeField] float scaleSpeed = 7f;
		[SerializeField] GameObject lostInstance;
		[SerializeField] float buttonsAppearDelay = 2f;

		[SerializeField] GameObject lostButtonsParent;
		[SerializeField] SO.IntVariable deaths;
		[SerializeField] TMPro.TextMeshProUGUI deathText;


		RectTransform lostTransform;
		
		RawImage lostImage;
		CanvasGroup lostGroup;
		bool lost = false;
		bool lost2 = false;
		public float initialDelay = 2f;
		float timer = 0;

		float LostScale
		{
			get { return lostTransform.localScale.x; }
			set {
				lostTransform.gameObject.SetActive(value > 0 ? true : false);
				lostTransform.localScale = new Vector3(value, value, value);
			}
		}

		float LostAlpha
		{
			get { if (!lostInstance.activeInHierarchy) { return 0; }; return lostGroup.alpha; }
			set {
				lostInstance.SetActive(value > 0 ? true : false);
				lostGroup.alpha = value;
			}
		}

		private void Start()
		{
			lostTransform = lostButtonsParent.GetComponent<RectTransform>();
			if (lostInstance != null)
				lostGroup = lostInstance.GetComponent<CanvasGroup>();
			lost = false;
			lost2 = false;
			deaths.value = 0;
			timer = 0;
			LostScale = 0;

		}

		private void Update()
		{
			

			if (lost)
			{
				timer += Time.deltaTime;
				if (timer > initialDelay)
				{
					//LostAlpha = Mathf.Lerp(LostAlpha, 1, lostSpeed * Time.deltaTime);
					LostAlpha = Mathf.MoveTowards(LostAlpha, 1, lostSpeed * Time.deltaTime);
					if (timer > buttonsAppearDelay + initialDelay)
					{
						if (!lost2)
						{
							lost2 = true;
						}
						LostScale = Mathf.Lerp(LostScale, 1, scaleSpeed * Time.deltaTime);
					}
					deathText.text = "Deaths:  " + deaths.value;
				}
			}
		}

		public void LostGame()
		{
			lost = true;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		public void EnterEndlessMode()
		{
			ResetVariables();
		}

		public void ResetVariables()
		{
			lost = false;
			lost2 = false;
			timer = 0;
			LostScale = 0;
			LostAlpha = 0;
		}
	}
}