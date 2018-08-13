using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SIS.States;

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

		//Sets hidden variables to state actions show in GUI
		protected override void SetParentActions()
		{
			State<Example> parent = this;
			parent.onUpdate = onUpdate;
			parent.onFixed = onFixed;
			parent.onEnter = onEnter;
			parent.onExit = onExit;
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
	public class ExampleTransition : Transition<Example>{ }
}