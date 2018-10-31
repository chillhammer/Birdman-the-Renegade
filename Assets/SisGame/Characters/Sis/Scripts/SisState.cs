using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SIS.States;
using System.Linq;

namespace SIS.Characters.Sis
{
	[CreateAssetMenu(menuName = "Characters/Sis/Sis State")]
	public class SisState : State<Sis>
	{

		public new SisStateActions[] onFixed;
		public new SisStateActions[] onUpdate;
		public new SisStateActions[] onEnter;
		public new SisStateActions[] onExit;

		[SerializeField]
		public new List<SisTransition> transitions = new List<SisTransition>();

		protected override void SetParentActions()
		{
			State<Sis> parent = (State<Sis>)this;
			parent.onUpdate = onUpdate;
			parent.onFixed = onFixed;
			parent.onEnter = onEnter;
			parent.onExit = onExit;
			parent.transitions = transitions.Cast<Transition<Sis>>().ToList();
			for (int i = 0; i < parent.transitions.Count; ++i)
			{
				parent.transitions[i].condition = transitions[i].condition;
				parent.transitions[i].targetState = transitions[i].targetState;
			}
		}

		public override void Tick(Sis owner)
		{
			base.Tick(owner);
			SetParentActions();
		}
	}

	//Allows to be shown in GUI since Unity is anti-generics in the inspector
	public abstract class SisStateActions : StateActions<Sis> { }
	public abstract class SisCondition : Condition<Sis> { }
	[System.Serializable]
	public class SisTransition : Transition<Sis>
	{
		public new SisCondition condition;
		public new SisState targetState;
	}
}