using UnityEngine;
using UnityEditor;
using SIS.States.Actions;

namespace SIS.Characters.Sis
{
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/State Action Composite")]
	public class SisStateActionComposite : SisStateActions {
		public SisComposite action;

		public override void Execute(Sis owner)
		{
			action.Execute(owner);
		}

		[System.Serializable]
		public class SisComposite : StateActionComposite<Sis, SisStateActions> { }
	}
}