using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.States
{
    [CreateAssetMenu]
    public class State<M> : ScriptableObject where M : StateMachine
    {
    	public StateActions<M>[] onFixed;
        public StateActions<M>[] onUpdate;
        public StateActions<M>[] onEnter;
        public StateActions<M>[] onExit;

        public int idCount;
		[SerializeField]
        public List<Transition> transitions = new List<Transition>();

		#region Enter State
		public void OnEnter(M owner)
        {
            ExecuteActions(owner, onEnter);
        }
		#endregion

		#region Update State
		public void FixedTick(M owner)
		{
			ExecuteActions(owner, onFixed);
		}

        public void Tick(M owner)
        {
            ExecuteActions(owner, onUpdate);
            CheckTransitions(owner);
        }
		#endregion

		#region Exit State
		public void OnExit(M states)
        {
            ExecuteActions(states, onExit);
        }
		#endregion

		//Helper Functions
		public void CheckTransitions(M states)
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].disable)
                    continue;

                if(transitions[i].condition.CheckCondition(states))
                {
                    if (transitions[i].targetState != null)
                    {
                        states.currentState = transitions[i].targetState;
                        OnExit(states);
                        states.currentState.OnEnter(states);
                    }
                    return;
                }
            }
        }
        
        public void ExecuteActions(M owner, StateActions<M>[] actions)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                if (actions[i] != null)
					actions[i].Execute(owner);
            }
        }

        public Transition AddTransition()
        {
            Transition retVal = new Transition();
            transitions.Add(retVal);
            retVal.id = idCount;
            idCount++;
            return retVal;
        }

        public Transition GetTransition(int id)
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].id == id)
                    return transitions[i];
            }

            return null;
        }

		public void RemoveTransition(int id)
		{
			Transition t = GetTransition(id);
			if (t != null)
				transitions.Remove(t);
		}

    }
}
