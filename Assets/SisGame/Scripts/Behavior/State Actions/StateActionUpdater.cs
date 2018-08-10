using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SA
{
	[CreateAssetMenu(menuName = "Actions/State Actions/State Action Holder")]
	public class StateActionUpdater : StateActions
	{
		public StateActions[] actions;

		public override void Execute(StateManager stateManager)
		{
			if (actions == null)
				return;

			for (int i = 0; i < actions.Length; ++i)
			{
				actions[i].Execute(stateManager);
			}
		}
	}
}