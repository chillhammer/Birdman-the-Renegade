using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SIS.Menu
{
	public class OptionsButtonFunctionality : MonoBehaviour
	{
		public GameObject optionsPanel;
		Image panelImage;
		public bool OptionsUp { get; set; }
		public float OptionsScale { get { return panelImage.rectTransform.localScale.x;  } }
		[SerializeField] float scaleSpeed = 5;

		public CreditsButtonFunctionality creditsButton;
		public Slider soundSlider;
		public Slider musicSlider;
		public Slider sensitivitySlider;

		public SO.FloatVariable sensitivityVariable;

		private void Start()
		{
			if (optionsPanel != null)
				panelImage = optionsPanel.GetComponent<Image>();
			soundSlider.value = Managers.GameManagers.AudioManager.SoundVolume;
			musicSlider.value = Managers.GameManagers.AudioManager.MusicVolume;
			sensitivitySlider.value = sensitivityVariable.value - 0.5f;
		}

		// Update is called once per frame
		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (OptionsUp)
				{
					//if (!panelImage.rectTransform.rect.Contains(Input.mousePosition))
						//optionsPanel.SetActive(false);
				}
			}
			if (panelImage != null)
			{
				float scale = Mathf.Lerp(OptionsScale, OptionsUp ? 1 : 0, scaleSpeed * Time.deltaTime);
				panelImage.rectTransform.localScale = new Vector3(scale, scale, scale);
				if (scale == 0)
				{
					optionsPanel.SetActive(false);
				}
			}
		}

		public void ToggleOptions()
		{
			OptionsUp = !OptionsUp;
			if (OptionsUp)
			{
				creditsButton.CreditsUp = false;
				optionsPanel.SetActive(true);
			}
		}

		//Called by Sliders
		public void SetSound()
		{
			Managers.GameManagers.AudioManager.SoundVolume = soundSlider.value;
		}

		public void SetMusic()
		{
			Managers.GameManagers.AudioManager.MusicVolume = musicSlider.value;
		}

		public void SetSensitivity()
		{
			sensitivityVariable.value = sensitivitySlider.value + 0.5f;
		}
	}
}