using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;


namespace SIS.Characters.Ham
{
	//Since Generic Variables Cannot Be Shown in Inspector
	[CreateAssetMenu(menuName = "Characters/Hammy/Editor/Settings")]
	public class HammyEditorSettings : EditorSettings<Hammy>
	{
		public HammyBehaviorGraph HammyCurrentGraph;
		public HammyStateNode HammyStateNode;
		public HammyPortalNode HammyPortalNode;
		public HammyTransitionNode HammyTransitionNode;
		public HammyCommentNode HammyCommentNode;

		public void SetParentNodes()
		{
			currentGraph = HammyCurrentGraph;
			stateNode = HammyStateNode;
			portalNode = HammyPortalNode;
			transitionNode = HammyTransitionNode;
			commentNode = HammyCommentNode;

		}
	}
}
