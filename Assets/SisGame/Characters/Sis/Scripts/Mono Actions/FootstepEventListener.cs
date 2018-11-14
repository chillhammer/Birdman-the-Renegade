using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Actions;

namespace SIS.Characters.Sis
{
	//Listens for event from animator
	public class FootstepEventListener : MonoBehaviour
	{
		public SisVariable sis;

		public void Footstep(string foot)
		{
			if (sis.value != null)
			{
				sis.value.OnFootstep(foot);
			}
			else
			{
				Debug.LogWarning("Sis not defined!");
			}
		}
	}
}