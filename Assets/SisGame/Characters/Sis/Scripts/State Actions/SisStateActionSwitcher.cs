using UnityEngine;
using UnityEditor;
using SIS.States.Actions;

namespace SIS.Characters.Sis
{
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/State Action Switcher")]
	public class SisStateActionSwitcher : StateActionSwitcher<Sis, SisStateActions> { }
}