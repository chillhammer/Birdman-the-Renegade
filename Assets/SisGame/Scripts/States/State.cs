using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.States
{
    [CreateAssetMenu]
    public class State<C> : ScriptableObject where C : Character
    {
    	public StateActions<C>[] onFixed;
        public StateActions<C>[] onUpdate;
        public StateActions<C>[] onEnter;
        public StateActions<C>[] onExit;

        public int idCount;
		[SerializeField]
        public List<Transition<M>> transitions = new List<Transition<M>>();

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
		public void CheckTransitions(M owner)
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].disable)
                    continue;

                if(transitions[i].condition.CheckCondition(owner))
                {
                    if (transitions[i].targetState != null)
                    {
						owner.currentState = transitions[i].targetState;
                        OnExit(owner);
						owner.currentState.OnEnter(owner);
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
