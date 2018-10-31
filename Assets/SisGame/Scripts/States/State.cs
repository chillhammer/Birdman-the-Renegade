using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;
using System;

namespace SIS.States
{
    //[CreateAssetMenu]
    public abstract class State<C> : ScriptableObject where C : Character
    {
    	public StateActions<C>[] onFixed;
        public StateActions<C>[] onUpdate;
        public StateActions<C>[] onEnter;
        public StateActions<C>[] onExit;


        public int idCount;

		[SerializeField]
        public List<Transition<C>> transitions = new List<Transition<C>>();

		//Polymorphism
		protected abstract void SetParentActions();
		private void OnEnable()
		{
			SetParentActions();
		}


		#region Enter State
		public void OnEnter(C owner)
        {
            ExecuteActions(owner, onEnter);
        }
		#endregion

		#region Update State
		public void FixedTick(C owner)
		{
			ExecuteActions(owner, onFixed);
		}

        public virtual void Tick(C owner)
        {
            ExecuteActions(owner, onUpdate);
            CheckTransitions(owner);
        }
		#endregion

		#region Exit State
		public void OnExit(C owner)
        {
            ExecuteActions(owner, onExit);
        }
		#endregion

		//Helper Functions
		public void CheckTransitions(C owner)
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].disable)
                    continue;

                if(transitions[i].condition.CheckCondition(owner))
                {
                    if (transitions[i].targetState != null)
                    {
						OnExit(owner);

						owner.ChangeState(i); //Workaround since states are shared
                    }
                    return;
                }
            }
        }
        
        public void ExecuteActions(C owner, StateActions<C>[] actions)
        {
			if (actions == null) return;
            for (int i = 0; i < actions.Length; i++)
            {
                if (actions[i] != null)
					actions[i].Execute(owner);
            }
        }

        public virtual Transition<C> AddTransition()
        {
            Transition<C> retVal = new Transition<C>();
            transitions.Add(retVal);
            retVal.id = idCount;
            idCount++;
            return retVal;
        }

        public virtual Transition<C> GetTransition(int id)
        {
            for (int i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].id == id)
                    return transitions[i];
            }

            return null;
        }

		public virtual void RemoveTransition(int id)
		{
			Transition<C> t = GetTransition(id);
			if (t != null)
				transitions.Remove(t);
		}

    }
}
