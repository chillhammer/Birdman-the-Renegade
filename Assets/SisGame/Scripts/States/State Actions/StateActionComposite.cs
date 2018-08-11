using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.States.Actions
{
	[CreateAssetMenu(menuName = "States/State Actions/State Action Composite")]
	public class StateActionComposite<C> : StateActions<C> where C : Character
	{
		public StateActions<C>[] actions;

		public override void Execute(C owner)
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