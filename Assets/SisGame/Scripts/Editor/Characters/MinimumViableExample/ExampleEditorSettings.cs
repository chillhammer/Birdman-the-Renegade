using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;


namespace SIS.Characters.Example
{
	//Since Generic Variables Cannot Be Shown in Inspector
	[CreateAssetMenu(menuName = "Characters/Example/Editor/Settings")]
	public class ExampleEditorSettings : EditorSettings<Example>
	{
		public ExampleBehaviorGraph exampleCurrentGraph;
		public ExampleStateNode exampleStateNode;
		public ExamplePortalNode examplePortalNode;
		public ExampleTransitionNode exampleTransitionNode;
		public ExampleCommentNode exampleCommentNode;

		public void SetParentNodes()
		{
			currentGraph = exampleCurrentGraph;
			stateNode = exampleStateNode;
			portalNode = examplePortalNode;
			transitionNode = exampleTransitionNode;
			commentNode = exampleCommentNode;

		}
	}
}
