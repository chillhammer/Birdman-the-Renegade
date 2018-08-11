using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SIS.Characters;

namespace SIS.BehaviorEditor
{
    public abstract class DrawNode<C> : ScriptableObject where C : Character
    {
        public abstract void DrawWindow(BaseNode<C> b);
        public abstract void DrawCurve(BaseNode<C> b);
    }
}
