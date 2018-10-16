using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SIS.States;
using System.Linq;

//Unity does not show C# Generics in the Inspector
//Thus, we must inherit from Generic and then expose custom variables and then set the originals
//We can have mutltiple classes in one file here as ExampleState is the only one that's a ScriptableObject

namespace SIS.Characters.Robo
{
	[CreateAssetMenu(menuName = "Characters/RoboPadron/RoboPadron State")]
	public class RoboPadronState : State<RoboPadron>
	{
		//Allows for overriden state actions to be in GUI
		public new RoboPadronStateActions[] onFixed;
		public new RoboPadronStateActions[] onUpdate;
		public new RoboPadronStateActions[] onEnter;
		public new RoboPadronStateActions[] onExit;

		[SerializeField]
		public new List<RoboPadronTransition> transitions = new List<RoboPadronTransition>();

		//Sets hidden variables to state actions show in GUI
		protected override void SetParentActions()
		{
			State<RoboPadron> parent = this;
			parent.onUpdate = onUpdate;
			parent.onFixed = onFixed;
			parent.onEnter = onEnter;
			parent.onExit = onExit;
			parent.transitions = transitions.Cast<Transition<RoboPadron>>().ToList();
			for (int i = 0; i < parent.transitions.Count; ++i)
			{
				parent.transitions[i].condition = transitions[i].condition;
				parent.transitions[i].targetState = transitions[i].targetState;
			}
		}

		public override void Tick(RoboPadron owner)
		{
			base.Tick(owner);
			SetParentActions();
		}
	}

	public abstract class RoboPadronStateActions : StateActions<RoboPadron> { }
	public abstract class RoboPadronCondition : Condition<RoboPadron> { }
	[System.Serializable]
	public class RoboPadronTransition : Transition<RoboPadron> {
		public new RoboPadronCondition condition;
		public new RoboPadronState targetState;
	}
}