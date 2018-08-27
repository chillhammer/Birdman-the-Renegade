using UnityEngine;
using UnityEditor;
using SIS.States.Actions;

//General actions, found in SisGame/Scripts/States/State Actions
//	can be inherited to get some baseline functionality
//	must be in it it's own file to get avoid ScriptableObject corruption

namespace SIS.Characters.Robo
{
	//Composite allows to group multiple actions into one
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/State Action Switcher")]
	public class RoboPadronStateActionComposite : RoboPadronStateActions
	{
		public RoboPadronComposite action;

		public override void Execute(RoboPadron owner)
		{
			action.Execute(owner);
		}

		[System.Serializable]
		public class RoboPadronComposite : StateActionComposite<RoboPadron, RoboPadronStateActions> { }
	}
}