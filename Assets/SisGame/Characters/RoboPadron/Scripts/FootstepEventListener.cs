using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Actions;

namespace SIS.Characters.Robo
{
	//Listens for event from animator
	public class FootstepEventListener : MonoBehaviour {

		RoboPadron robo;

		void Start() {
			robo = transform.parent.GetComponent<RoboPadron>();
		}

		public void Footstep(string foot)
		{
			robo.OnFootstep();
		}
	}
}