using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.States.Actions
{
	[CreateAssetMenu(menuName = "Actions/Mono Actions/Set Bool And")]
	public class SetBoolAnd : SIS.Actions.Action
	{
		public List<SO.BoolVariable> bools;
		public SO.BoolVariable targetBool;

		public override void Execute()
		{
			foreach (SO.BoolVariable boolVar in bools)
			{
				if (!boolVar.value)
				{
					targetBool.value = false;
					return;
				}
			}
			targetBool.value = true;

		}
	}
}
