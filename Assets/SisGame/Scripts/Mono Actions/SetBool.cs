using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.States.Actions
{
	[CreateAssetMenu(menuName = "Actions/Mono Actions/Set Bool")]
	public class SetBool : SIS.Actions.Action
	{
		public bool boolVar;
		public SO.BoolVariable targetBool;

		public override void Execute()
		{
			
			targetBool.value = boolVar;

		}
	}
}
