using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Map
{
	public class BirdmanHead : MonoBehaviour
	{
		public SO.TransformVariable sisTransform;
		// Use this for initialization
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{
			if (sisTransform.value == null)
				return;
			Quaternion towardsPlayer = 
				Quaternion.LookRotation(sisTransform.value.position - transform.position, Vector3.left);
			transform.rotation = towardsPlayer;
		}
	}
}