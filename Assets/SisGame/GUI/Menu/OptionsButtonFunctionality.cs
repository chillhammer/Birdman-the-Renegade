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
		public bool OptionsUp { get { return optionsPanel.activeInHierarchy; } }

		private void Start()
		{
			panelImage = optionsPanel.GetComponent<Image>();
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
		}

		public void ToggleOptions()
		{
			optionsPanel.SetActive(!OptionsUp);
		}
	}
}