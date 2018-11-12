using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters {
	public class CharacterHealthBarController : MonoBehaviour {

		[SerializeField] Character character;
		[SerializeField] Sis.SisVariable sisVariable;
		[SerializeField] UnityEngine.UI.RawImage healthbarImage;
		[SerializeField] AnimationCurve hitShake;
		[SerializeField] bool destroyOnZero;
		[Range(0,1)]
		[SerializeField] float shakeIntensity = 0.6f;
		[SerializeField] float shakeTime = 1f;
		[SerializeField] float healthLerpSpeed = 3f;

		float maxHealth;
		float maxHealthWidth;
		float targetWidth;

		public float HealthBarWidth {
			get { return healthbarImage.rectTransform.rect.width; }
			set { healthbarImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,value);}
		}


		// Use this for initialization
		void Start() {
			if (sisVariable != null)
			{
				SetCharacter(sisVariable.value);
			}
			maxHealth = character.health;
			maxHealthWidth = HealthBarWidth;
			targetWidth = maxHealthWidth;
		}

		private void OnEnable()
		{
			if (character != null)
				character.onHitDelegate += OnHit; //Update when hit
		}
		private void OnDisable()
		{
			if (character != null)
				character.onHitDelegate -= OnHit;
		}

		public void SetCharacter(Character c)
		{
			if (character != null)
			{
				character.onHitDelegate -= OnHit;
			}
			character = c;
			if (character != null)
			{
				character.onHitDelegate += OnHit; //Update when hit
			}
		}

		private void Update()
		{
			HealthBarWidth = Mathf.Lerp(HealthBarWidth, targetWidth, healthLerpSpeed * Time.deltaTime);
			if (HealthBarWidth <= 0.01f && destroyOnZero)
			{
				Destroy(gameObject);
			}
		}

		protected virtual void OnHit()
		{
			//Debug.Log("Change HB GUI. Health: " + character.health + ". MaxHealth: " + maxHealth + ". MaxHealthWidth: " + maxHealthWidth);
			UpdateWidth();
			StartCoroutine("ShakeBar");
		}

		public void UpdateWidth()
		{
			targetWidth = character.health / maxHealth * maxHealthWidth;
		}

		IEnumerator ShakeBar()
		{
			float timer = 0;
			while (timer < shakeTime)
			{
				timer += Time.deltaTime;
				float offsetX = shakeIntensity * hitShake.Evaluate(timer / shakeTime);
				transform.localPosition = transform.right * offsetX;
				yield return null;
			}

		}
	}
}