using UnityEngine;
using UnityEditor;
using SIS.States.Actions;

namespace SIS.Characters.Sis
{
	[CreateAssetMenu(menuName = "Characters/Sis/State Actions/State Action Composite")]
	public class SisStateActionComposite : StateActionComposite<Sis, SisStateActions> { }
}