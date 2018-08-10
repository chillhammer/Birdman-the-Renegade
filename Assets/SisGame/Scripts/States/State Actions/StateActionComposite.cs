using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.States.Actions
{
	[CreateAssetMenu(menuName = "States/State Actions/State Action Composite")]
	public class StateActionComposite<M> : StateActions<M> where M : StateMachine
	{
		public StateActions<M>[] actions;

		public override void Execute(M owner)
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