using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters {
	public class CharacterHealthBarController : MonoBehaviour {

		[SerializeField] Character character;
		[SerializeField] UnityEngine.UI.RawImage healthbarImage;
		[SerializeField] AnimationCurve hitShake;

		float maxHealth;
		float maxHealthWidth;
		float targetWidth;

		public float HealthBarWidth {
			get { return healthbarImage.rectTransform.rect.width; }
			set { healthbarImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,value);}
		}


		// Use this for initialization
		void Start() {
			maxHealth = character.health;
			maxHealthWidth = HealthBarWidth;
			targetWidth = maxHealthWidth;
		}

		private void OnEnable()
		{
			character.onHitDelegate += OnHit; //Update when hit
		}
		private void OnDisable()
		{
			character.onHitDelegate -= OnHit;
		}

		private void Update()
		{
			HealthBarWidth = Mathf.Lerp(HealthBarWidth, targetWidth, 3f * Time.deltaTime);
			if (HealthBarWidth <= 0.01f)
			{
				Destroy(gameObject);
			}
		}

		protected virtual void OnHit()
		{
			//Debug.Log("Change HB GUI. Health: " + character.health + ". MaxHealth: " + maxHealth + ". MaxHealthWidth: " + maxHealthWidth);
			targetWidth = character.health / maxHealth * maxHealthWidth;
			StartCoroutine("ShakeBar");
		}

		IEnumerator ShakeBar()
		{
			float timer = 0;
			float seconds = 1;
			while (timer < seconds)
			{
				timer += Time.deltaTime;
				float offsetX = 0.6f * hitShake.Evaluate(timer / seconds);
				transform.localPosition = transform.right * offsetX;
				yield return null;
			}

		}
	}
}