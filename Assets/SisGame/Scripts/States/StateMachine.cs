using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.States
{
	public class StateMachine<C> where C : Character
	{
		public float delta; //calculated with Time.delta

		//Passed in from Character owner
		private C owner;
		public State<C> currentState;
		private StateActions<C> initActionBatch;

		private StateMachine() {}
		public StateMachine(C owner, State<C> startingState, StateActions<C> initActionBatch)
		{
			this.owner = owner;
			currentState = startingState;
			if (initActionBatch != null)
			{
				initActionBatch.Execute(owner);
			}
		}

		//Run Logic for Current State
		public void FixedTick()
		{
			delta = Time.fixedDeltaTime;
			if (currentState != null)
			{
				currentState.FixedTick(owner);
			}
		}

		public void Tick()
		{
			delta = Time.deltaTime;
			if (currentState != null)
			{
				currentState.Tick(owner);
			}
		}
    }
}
