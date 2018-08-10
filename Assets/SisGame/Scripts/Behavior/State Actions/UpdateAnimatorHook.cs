using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu(menuName = "Actions/State Actions/Update Animator Hook")]
	public class UpdateAnimatorHook : StateActions
	{
		public override void Execute(StateManager stateManager)
		{
			stateManager.animHook.Tick();
		}
	}
}