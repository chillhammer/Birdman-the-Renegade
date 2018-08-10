using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.States
{
    public abstract class StateActions<C> : ScriptableObject where C : Character
    {
        public abstract void Execute(C owner);
    }
}
