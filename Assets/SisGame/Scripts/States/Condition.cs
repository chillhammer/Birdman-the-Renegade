using UnityEngine;
using SIS.Characters;

namespace SIS.States
{
    public abstract class Condition<C	> : ScriptableObject where C : Character
    {
		public string description;

        public abstract bool CheckCondition(C owner);

    }
}
