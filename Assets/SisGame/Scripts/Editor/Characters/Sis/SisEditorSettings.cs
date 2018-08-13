using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;


namespace SIS.Characters.Sis
{
	//Since Generic Variables Cannot Be Shown in Inspector
	[CreateAssetMenu(menuName = "Characters/Sis/Editor/Settings")]
	public class SisEditorSettings : EditorSettings<Sis>
	{
		public SisBehaviorGraph sisCurrentGraph;
		public SisStateNode sisStateNode;
		public SisPortalNode sisPortalNode;
		public SisTransitionNode sisTransitionNode;
		public SisCommentNode sisCommentNode;

		public void SetParentNodes()
		{
			currentGraph = sisCurrentGraph;
			stateNode = sisStateNode;
			portalNode = sisPortalNode;
			transitionNode = sisTransitionNode;
			commentNode = sisCommentNode;

		}
	}
}
