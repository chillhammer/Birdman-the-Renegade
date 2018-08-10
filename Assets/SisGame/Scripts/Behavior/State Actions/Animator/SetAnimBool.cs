using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu(menuName = "Actions/State Actions/Set Animation Bool")]
	public class SetAnimBool : StateActions
	{
		public string targetBool;
		public bool status;

		public override void Execute(StateManager stateManager)
		{
			if (stateManager.anim == null)
			{
				Debug.Log("No Animator Found on State Manager");
				return;
			}
			stateManager.anim.SetBool(targetBool, status);

		}
	}
}
