using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.States.Actions
{
	//[CreateAssetMenu(menuName = "States/State Actions/State Action Composite")]
	public class StateActionComposite<C, SA> where C : Character
											 where SA : StateActions<C>
	{
		public SA[] actions;

		public void Execute(C owner)
		{
			if (actions == null)
				return;

			for (int i = 0; i < actions.Length; ++i)
			{
				actions[i].Execute(owner);
			}
		}
	}
}