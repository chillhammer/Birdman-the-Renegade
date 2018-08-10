using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Actions.Input
{
	[CreateAssetMenu(menuName = "Inputs/Input Updater")]
	public class InputUpdater : Action
	{
		public Action[] inputs;
		public override void Execute()
		{
			if (inputs != null)
			{
				for (int i = 0; i < inputs.Length; ++i)
				{
					inputs[i].Execute();
				}
			}
		}
	}
}
