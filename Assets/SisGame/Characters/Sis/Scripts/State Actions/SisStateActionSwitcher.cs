using UnityEngine;
using UnityEditor;
using SIS.States.Actions;

namespace SIS.Characters.Sis
{
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/State Action Switcher")]
	public class SisStateActionSwitcher : SisStateActions {
		public SisSwitcher switcher;

		public override void Execute(Sis owner)
		{
			switcher.Execute(owner);
		}

		[System.Serializable]
		public class SisSwitcher : StateActionSwitcher<Sis, SisStateActions> { }
	}

	
}