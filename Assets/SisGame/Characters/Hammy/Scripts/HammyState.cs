using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SIS.States;
using System.Linq;

//Unity does not show C# Generics in the Inspector
//Thus, we must inherit from Generic and then expose custom variables and then set the originals
//We can have mutltiple classes in one file here as HammyState is the only one that's a ScriptableObject
//Note I comment out CreateAssetMenu to remove clutter from Unity, but it is necessary

namespace SIS.Characters.Ham
{
	[CreateAssetMenu(menuName = "Characters/Hammy/Hammy State")]
	public class HammyState : State<Hammy>
	{
		//Allows for overriden state actions to be in GUI
		public new HammyStateActions[] onFixed;
		public new HammyStateActions[] onUpdate;
		public new HammyStateActions[] onEnter;
		public new HammyStateActions[] onExit;
		[SerializeField]
		public new List<HammyTransition> transitions = new List<HammyTransition>();

		//Sets hidden variables to state actions show in GUI
		protected override void SetParentActions()
		{
			State<Hammy> parent = this;
			parent.onUpdate = onUpdate;
			parent.onFixed = onFixed;
			parent.onEnter = onEnter;
			parent.onExit = onExit;
			parent.transitions = transitions.Cast<Transition<Hammy>>().ToList();
			for (int i = 0; i < parent.transitions.Count; ++i)
			{
				parent.transitions[i].condition = transitions[i].condition;
				parent.transitions[i].targetState = transitions[i].targetState;
			}
		}

		public override void Tick(Hammy owner)
		{
			base.Tick(owner);
			SetParentActions();
		}
	}

	public abstract class HammyStateActions : StateActions<Hammy> { }
	public abstract class HammyCondition : Condition<Hammy> { }

	[System.Serializable]
	public class HammyTransition : Transition<Hammy>{
		public new HammyCondition condition;
		public new HammyState targetState;
	}
}