using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SIS.Actions
{
    public abstract class Action : ScriptableObject
    {
        public abstract void Execute();
    }
}
