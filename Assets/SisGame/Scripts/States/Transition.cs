using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.States
{
    [System.Serializable]
    public class Transition<C> where C : Character
    {
        public int id;
        public Condition<C> condition;
        public State<C> targetState;
        public bool disable;
    }
}
