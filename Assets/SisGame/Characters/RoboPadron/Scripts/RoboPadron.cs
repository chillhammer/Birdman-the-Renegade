using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SIS.States;
using SIS.Items;

namespace SIS.Characters.Robo
{
	public class RoboPadron : Character
	{
		#region StateMachine Setup
		//State Actions use this instead of Time.delta
		public float delta { get { return stateMachine.delta; } }

		//Must Start Off in a State
		[SerializeField] private RoboPadronState startingState; 

		//Optional. Use StateActionComposite to run multiple actions on create
		[SerializeField] private RoboPadronStateActions initActionsBatch; 

		public StateMachine<RoboPadron> stateMachine;

		private new void Start()
		{
			base.Start();
			stateMachine = new StateMachine<RoboPadron>(this, startingState, initActionsBatch);

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

		#endregion

		public float health = 1;

		//Allows for initial setup, better to use InitActionBatch, but it's here if you don't want to make action
		protected override void SetupComponents()
		{
			//Example: TrajectorySystem.Init()
		}
	}
}