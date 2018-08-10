using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.States
{
    public abstract class Condition<M> : ScriptableObject where M : StateMachine
    {
		public string description;

        public abstract bool CheckCondition(M owner);

    }
}
