using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SIS.Characters;
using SIS.States;

namespace SIS.BehaviorEditor
{
	//[CreateAssetMenu(menuName = "Editor/Nodes/Portal Node")]
	public class PortalNode<C> : DrawNode<C> where C : Character
	{

		public override void DrawCurve(BaseNode<C> b)
		{

		}

		public override void DrawWindow(BaseNode<C> b)
		{
			b.stateRef.currentState = (State<C>)EditorGUILayout.ObjectField(b.stateRef.currentState, typeof(State<C>), false);
			b.isAssigned = b.stateRef.currentState != null;

			if (b.stateRef.previousState != b.stateRef.currentState)
			{
				b.stateRef.previousState = b.stateRef.currentState;
				BehaviorEditor<C>.forceSetDirty = true;
			}
		}
	}
}
