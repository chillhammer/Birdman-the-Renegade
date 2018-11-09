using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Menu
{
	public class TopHalfSisHover : MonoBehaviour
	{
		public float amount = 1f;
		public float speed = 0.01f;
		float startY;
		// Use this for initialization
		void Start()
		{
			startY = transform.position.y;
		}

		// Update is called once per frame
		void Update()
		{
			Vector3 pos = transform.localPosition;
			pos.y = -(Mathf.Sin(Time.realtimeSinceStartup * speed) + 1) * 0.5f * amount;
			transform.localPosition = pos;
		}
	}
}
