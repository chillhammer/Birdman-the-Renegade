using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu(menuName = "Actions/State Actions/Movement Animations")] 
	public class MovementAnimations : StateActions
	{
		public string floatName;
		public override void Execute(StateManager stateManager)
		{
			if (stateManager.anim == null)
				return;
			stateManager.anim.SetFloat(floatName, stateManager.movementValues.moveAmount, 0.2f, stateManager.delta);

		}
	}
}