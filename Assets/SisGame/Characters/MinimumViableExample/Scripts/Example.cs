using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SIS.States;
using SIS.Items;

//Assume everythin in the MinimumViableExample is mandatory to get a StateMachine character running
//	unless specified Optional

//To get the Behavior Editor working, you can go to SisGame/Editor/BehaviorEditor/Characters/Sis
//	duplicate and then just replace Sis with Example

namespace SIS.Characters.Example
{
	//
	public class Example : Character
	{
		#region StateMachine Setup

		//Must Start Off in a State
		[SerializeField] private ExampleState startingState; 

		//Optional. Use StateActionComposite to run multiple actions on create
		[SerializeField] private ExampleStateActions initActionsBatch; 

		public StateMachine<Example> stateMachine;

		private new void Start()
		{
			base.Start();
			stateMachine = new StateMachine<Example>(this, startingState, initActionsBatch);

		}
		//Run State Machine Logic
		private void FixedUpdate()
		{
			stateMachine.FixedTick();
		}
		private void Update()
		{
			stateMachine.Tick();
		}
		public override void ChangeState(int transitionIndex)
		{
			var newState = stateMachine.currentState.transitions[transitionIndex].targetState;
			stateMachine.currentState = newState;
			stateMachine.currentState.OnEnter(this);
		}
		#endregion


		//Must atleast override since it is abstract
		//Allows for initial setup, better to use InitActionBatch, but it's here if you don't want to make action
		protected override void SetupComponents()
		{
			//Example: TrajectorySystem.Init()
		}
	}
}