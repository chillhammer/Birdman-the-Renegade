using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SIS.States;
using SIS.Characters;


namespace SIS.BehaviorEditor
{
	//[CreateAssetMenu(menuName ="Editor/Settings")]
	public class EditorSettings<C> : ScriptableObject where C : Character
	{
		public BehaviorGraph<C> currentGraph;
		public StateNode<C> stateNode;
		public PortalNode<C> portalNode;
		public TransitionNode<C> transitionNode;
		public CommentNode<C> commentNode;
		public bool makeTransition;
		public GUISkin skin;
		public GUISkin activeSkin;

		public BaseNode<C> AddNodeOnGraph(DrawNode<C> type, float width, float height, string title, Vector3 pos)
		{
			BaseNode<C> baseNode = new BaseNode<C>();
			baseNode.drawNode = type;
			baseNode.windowRect.width = width;
			baseNode.windowRect.height = height;
			baseNode.windowTitle = title;
			baseNode.windowRect.x = pos.x;
			baseNode.windowRect.y = pos.y;
			currentGraph.windows.Add(baseNode);
			baseNode.transRef = new TransitionNodeReferences<C>();
			baseNode.stateRef = new StateNodeReferences<C>();
			baseNode.id = currentGraph.idCount;
			currentGraph.idCount++;
			return baseNode;
		}
	}
}
