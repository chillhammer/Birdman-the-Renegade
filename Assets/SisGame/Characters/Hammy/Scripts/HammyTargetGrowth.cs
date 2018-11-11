using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Characters.Ham
{
	public class HammyTargetGrowth : MonoBehaviour
	{

		public float scaleTarget = 10;
		public float speed = 1;

		// Use this for initialization
		void Start()
		{
			SetScale(0);
		}

		// Update is called once per frame
		void Update()
		{
			SetScale(Mathf.Lerp(GetScale(), scaleTarget, speed * Time.deltaTime));
		}

		void SetScale(float scale)
		{
			transform.localScale = new Vector3(scale, transform.localScale.y, scale);
		}

		float GetScale()
		{
			return transform.localScale.x;
		}
	}
}