using UnityEngine;
using UnityEditor;
using SIS.States.Actions;

namespace SIS.Characters.Sis
{
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Execute Mono Action")]
	public class SisExecuteMonoAction : SisStateActions {
		public Actions.Action action;

		public override void Execute(Sis owner)
		{
			action.Execute();
		}
	}
}