using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.States.Actions
{
	//Decided action based on bool
	[CreateAssetMenu(menuName = "Actions/State Actions/Switcher")]
	public class StateActionSwitcher<M> : StateActions<M> where M : StateMachine
	{
		public SO.BoolVariable targetBool;
		public StateActions<M> onFalseAction;
		public StateActions<M> onTrueAction;

		public override void Execute(M owner)
		{
			if (targetBool.value)
			{
				if (onTrueAction != null)
					onTrueAction.Execute(owner);
			}
			else
			{
				if (onFalseAction != null)
					onFalseAction.Execute(owner);
			}
		}
	}
}