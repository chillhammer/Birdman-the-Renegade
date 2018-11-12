using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SIS.Menu
{
	public class CreditsButtonFunctionality : MonoBehaviour
	{
		public GameObject creditsPanel;
		Image panelImage;
		public bool CreditsUp { get; set; }
		public float CreditsScale { get { return panelImage.rectTransform.localScale.x;  } }
		[SerializeField] float scaleSpeed = 5;

		public OptionsButtonFunctionality optionsButton;

		private void Start()
		{
			panelImage = creditsPanel.GetComponent<Image>();
		}

		// Update is called once per frame
		void Update()
		{
			float scale = Mathf.Lerp(CreditsScale, CreditsUp ? 1 : 0, scaleSpeed * Time.deltaTime);
			panelImage.rectTransform.localScale = new Vector3(scale, scale, scale);
			if (scale == 0)
			{
				creditsPanel.SetActive(false);
			}
		}

		public void ToggleCredits()
		{
			Debug.Log("Toggled Credits");
			CreditsUp = !CreditsUp;
			if (CreditsUp)
			{
				optionsButton.OptionsUp = false;
				creditsPanel.SetActive(true);
			}
		}
	}
}