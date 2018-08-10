using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.States
{
    public abstract class StateActions<M> : ScriptableObject where M : StateMachine
    {
        public abstract void Execute(M owner);
    }
}
