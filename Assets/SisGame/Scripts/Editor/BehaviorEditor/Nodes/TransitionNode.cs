using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SIS.Characters;
using SIS.States;

namespace SIS.BehaviorEditor
{
    //[CreateAssetMenu(menuName ="Editor/Transition Node")]
    public class TransitionNode<C> : DrawNode<C> where C : Character
    {
      
        public void Init(StateNode<C> enterState, Transition<C> transition)
        {
      //      this.enterState = enterState;
        }

        public override void DrawWindow(BaseNode<C> b)
        {
            EditorGUILayout.LabelField("");
            BaseNode<C> enterNode = BehaviorEditor<C>.settings.currentGraph.GetNodeWithIndex(b.enterNode);
			if (enterNode == null)
			{
				return;
			}
			
			if (enterNode.stateRef.currentState == null)
			{
				BehaviorEditor<C>.settings.currentGraph.DeleteNode(b.id);
				return;
			}

            Transition<C> transition = enterNode.stateRef.currentState.GetTransition(b.transRef.transitionId);

			if (transition == null)
				return;


            transition.condition = 
                (Condition<C>)EditorGUILayout.ObjectField(transition.condition
                , typeof(Condition<C>), false);

            if(transition.condition == null)
            {            
                EditorGUILayout.LabelField("No Condition!");
                b.isAssigned = false;
            }
            else
            {

                b.isAssigned = true;
				if (b.isDuplicate)
				{
					EditorGUILayout.LabelField("Duplicate Condition!");
				}
				else
				{
					GUILayout.Label(transition.condition.description);

					BaseNode<C> targetNode = BehaviorEditor<C>.settings.currentGraph.GetNodeWithIndex(b.targetNode);
					if (targetNode != null)
					{
						if (!targetNode.isDuplicate)
							transition.targetState = targetNode.stateRef.currentState;
						else
							transition.targetState = null;
					}
					else
					{
						transition.targetState = null;
					}
				}
			}
            
            if (b.transRef.previousCondition != transition.condition)
            {
                b.transRef.previousCondition = transition.condition;
                b.isDuplicate = BehaviorEditor<C>.settings.currentGraph.IsTransitionDuplicate(b);
				
				if (!b.isDuplicate)
                {
					BehaviorEditor<C>.forceSetDirty = true;
					// BehaviorEditor.settings.currentGraph.SetNode(this);   
				}
            }
            
        }

        public override void DrawCurve(BaseNode<C> b)
        {
            Rect rect = b.windowRect;
            rect.y += b.windowRect.height * .5f;
            rect.width = 1;
            rect.height = 1;

            BaseNode<C> e = BehaviorEditor<C>.settings.currentGraph.GetNodeWithIndex(b.enterNode);
            if (e == null)
            {
                BehaviorEditor<C>.settings.currentGraph.DeleteNode(b.id);
            }
            else
            {
                Color targetColor = Color.green;
                if (!b.isAssigned || b.isDuplicate)
                    targetColor = Color.red;

                Rect r = e.windowRect;
                BehaviorEditor<C>.DrawNodeCurve(r, rect, true, targetColor);
            }

            if (b.isDuplicate)
                return;

            if(b.targetNode > 0)
            {
                BaseNode<C> t = BehaviorEditor<C>.settings.currentGraph.GetNodeWithIndex(b.targetNode);
                if (t == null)
                {
                    b.targetNode = -1;
                }
                else
                {
					

                    rect = b.windowRect;
                    rect.x += rect.width;
                    Rect endRect = t.windowRect;
                    endRect.x -= endRect.width * .5f;

                    Color targetColor = Color.green;

					if (t.drawNode is StateNode<C>)
					{
						if (!t.isAssigned || t.isDuplicate)
							targetColor = Color.red;
					}
					else
					{
						if (!t.isAssigned)
							targetColor = Color.red;
						else
							targetColor = Color.yellow;
					}
                    
                    BehaviorEditor<C>.DrawNodeCurve(rect,endRect,false, targetColor);
                }

            }
        }
    }
}
