using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;


namespace SIS.Characters.Robo
{
	//Since Generic Variables Cannot Be Shown in Inspector
	[CreateAssetMenu(menuName = "Characters/RoboPadron/Editor/Settings")]
	public class RoboPadronEditorSettings : EditorSettings<RoboPadron>
	{
		public RoboPadronBehaviorGraph roboCurrentGraph;
		public RoboPadronStateNode roboStateNode;
		public RoboPadronPortalNode roboPortalNode;
		public RoboPadronTransitionNode roboTransitionNode;
		public RoboPadronCommentNode roboCommentNode;

		public void SetParentNodes()
		{
			currentGraph = roboCurrentGraph;
			stateNode = roboStateNode;
			portalNode = roboPortalNode;
			transitionNode = roboTransitionNode;
			commentNode = roboCommentNode;

		}
	}
}
