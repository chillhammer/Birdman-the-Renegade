using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using SIS.Characters;
using SIS.States;

namespace SIS.BehaviorEditor
{
    [System.Serializable]
    public class BaseNode<C> where C : Character
    {
        public int id;
        public DrawNode<C> drawNode;
        public Rect windowRect;
        public string windowTitle;
        public int enterNode;
        public int targetNode;
        public bool isDuplicate;
        public string comment;
        public bool isAssigned;
		public bool showDescription;
		public bool isOnCurrent;

        public bool collapse;
		public bool showActions = true;
		public bool showEnterExit = false;
        [HideInInspector]
        public bool previousCollapse;

        [SerializeField]
        public StateNodeReferences<C> stateRef;
        [SerializeField]
        public TransitionNodeReferences<C> transRef;

        public void DrawWindow()
        {
            if(drawNode != null)
            {
                drawNode.DrawWindow(this);
            }
        }

        public void DrawCurve()
        {
            if (drawNode != null)
            {
                drawNode.DrawCurve(this);
            }
        }

    }

    [System.Serializable]
    public class StateNodeReferences<C> where C : Character
    { 
    //    [HideInInspector]
        public State<C> currentState;
        [HideInInspector]
        public State<C> previousState;
		public SerializedObject serializedState;
	    public ReorderableList onFixedList;
		public ReorderableList onUpdateList;
		public ReorderableList onEnterList;
		public ReorderableList onExitList;
	}

	[System.Serializable]
    public class TransitionNodeReferences<C> where C : Character
    {
        [HideInInspector]
        public Condition<C> previousCondition;
        public int transitionId;
    }
}
