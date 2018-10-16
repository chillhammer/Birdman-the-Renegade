using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SIS.States;
using System.Linq;

//Unity does not show C# Generics in the Inspector
//Thus, we must inherit from Generic and then expose custom variables and then set the originals
//We can have mutltiple classes in one file here as ExampleState is the only one that's a ScriptableObject
//Note I comment out CreateAssetMenu to remove clutter from Unity, but it is necessary

namespace SIS.Characters.Example
{
	//[CreateAssetMenu(menuName = "Characters/Example/Example State")]
	public class ExampleState : State<Example>
	{
		//Allows for overriden state actions to be in GUI
		public new ExampleStateActions[] onFixed;
		public new ExampleStateActions[] onUpdate;
		public new ExampleStateActions[] onEnter;
		public new ExampleStateActions[] onExit;
		[SerializeField]
		public new List<ExampleTransition> transitions = new List<ExampleTransition>();

		//Sets hidden variables to state actions show in GUI
		protected override void SetParentActions()
		{
			State<Example> parent = this;
			parent.onUpdate = onUpdate;
			parent.onFixed = onFixed;
			parent.onEnter = onEnter;
			parent.onExit = onExit;
			parent.transitions = transitions.Cast<Transition<Example>>().ToList();
			for (int i = 0; i < parent.transitions.Count; ++i)
			{
				parent.transitions[i].condition = transitions[i].condition;
				parent.transitions[i].targetState = transitions[i].targetState;
			}
		}

		public override void Tick(Example owner)
		{
			base.Tick(owner);
			SetParentActions();
		}
	}

	public abstract class ExampleStateActions : StateActions<Example> { }
	public abstract class ExampleCondition : Condition<Example> { }
	[System.Serializable]
	public class ExampleTransition : Transition<Example>{
		public new ExampleCondition condition;
		public new ExampleState targetState;
	}
}