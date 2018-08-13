using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.States.Actions
{
	//Decided action based on bool
	//[CreateAssetMenu(menuName = "Actions/State Actions/Switcher")]
	public class StateActionSwitcher<C, SA> : StateActions<C> where C : Character 
															  where SA : StateActions<C>
	{
		public SO.BoolVariable targetBool;
		public SA onFalseAction;
		public SA onTrueAction;

		public override void Execute(C owner)
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