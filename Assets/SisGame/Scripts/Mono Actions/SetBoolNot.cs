using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.States.Actions
{
	[CreateAssetMenu(menuName = "Actions/Mono Actions/Set Bool Not")]
	public class SetBoolNot : SIS.Actions.Action
	{
		public SO.BoolVariable boolVar;
		public SO.BoolVariable targetBool;

		public override void Execute()
		{
			targetBool.value = !boolVar.value;
		}
	}
}
