using UnityEngine;
using UnityEditor;
using SIS.States.Actions;

//General actions, found in SisGame/Scripts/States/State Actions
//	can be inherited to get some baseline functionality
//	must be in it it's own file to get avoid ScriptableObject corruption

namespace SIS.Characters.Example
{
	//Composite allows to group multiple actions into one
	//[CreateAssetMenu(menuName = "Characters/Sis/State Actions/State Action Switcher")]
	public class StateActionComposite : ExampleStateActions
	{
		public ExampleComposite action;

		public override void Execute(Example owner)
		{
			action.Execute(owner);
		}

		[System.Serializable]
		public class ExampleComposite : StateActionComposite<Example, ExampleStateActions> { }
	}
}