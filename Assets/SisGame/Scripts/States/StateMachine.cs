using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.States
{
    public class StateMachine<C> where C : Character
    {

        public State<C> currentState;
		public C owner;

		public StateActions<C> initActionBatch;
		public float delta;

		public void Init()
		{
			initActionBatch.Execute(owner);
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
