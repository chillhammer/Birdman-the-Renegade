using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters {
	public class CharacterHealthBarController : MonoBehaviour {

		[SerializeField] Character character;
		[SerializeField] UnityEngine.UI.RawImage healthbarImage;

		float maxHealth;
		float maxHealthWidth;


		// Use this for initialization
		void Start() {
			maxHealth = character.health;
			maxHealthWidth = healthbarImage.rectTransform.rect.width;
		}

		private void OnEnable()
		{
			character.onHitDelegate += OnHit; //Update when hit
		}
		private void OnDisable()
		{
			character.onHitDelegate -= OnHit;
		}

		protected void OnHit()
		{
			//Debug.Log("Change HB GUI. Health: " + character.health + ". MaxHealth: " + maxHealth + ". MaxHealthWidth: " + maxHealthWidth);
			healthbarImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 
				character.health / maxHealth * maxHealthWidth);
		}
	}
}