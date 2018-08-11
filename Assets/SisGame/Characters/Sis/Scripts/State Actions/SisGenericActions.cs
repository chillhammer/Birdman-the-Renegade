using UnityEngine;
using UnityEditor;
using SIS.States.Actions;

namespace SIS.Characters.Sis
{
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/Set Animation Bool")]
	public class SetAnimBool : SetAnimBool<Sis> { }

	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/State Action Switcher")]
	public class StateActionSwitcher : StateActionSwitcher<Sis> { }

	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/State Action Composite")]
	public class StateActionComposite : StateActionComposite<Sis> { }
}