using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;
using SIS.States;
using System.Collections.Generic;

namespace SIS.Characters.Sis
{
	public class SisBehaviorEditor : BehaviorEditor<Sis>
	{
		[MenuItem("Behavior Editor/Sis Editor")]
		static void ShowEditor()
		{
			editor = EditorWindow.GetWindow<SisBehaviorEditor>();
			editor.minSize = new Vector2(800, 600);
		}

		protected override void OnEnable()
		{
			Debug.Log("Attempting to find Settings");
			SisEditorSettings editorSettings = Resources.Load("SisEditorSettings") as SisEditorSettings;
			editorSettings.SetParentNodes();
			settings = editorSettings;
			style = settings.skin.GetStyle("window");
			activeStyle = settings.activeSkin.GetStyle("window");
		}
	}

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

	[CreateAssetMenu(menuName = "Characters/Sis/Editor/Nodes/Comment Node")]
	public class SisCommentNode : CommentNode<Sis>
	{
	}

	[CreateAssetMenu(menuName = "Characters/Sis/Editor/Nodes/Portal Node")]
	public class SisPortalNode : PortalNode<Sis>
	{
	}

	[CreateAssetMenu(menuName = "Characters/Sis/Editor/Nodes/State Node")]
	public class SisStateNode : StateNode<Sis>
	{
	}

	[CreateAssetMenu(menuName = "Characters/Sis/Editor/Nodes/Transition Node")]
	public class SisTransitionNode : TransitionNode<Sis>
	{
	}

	[CustomEditor(typeof(SisState))]
	public class SisStateGUI : CustomUI.StateGUI<Sis>
	{
	}

	[CreateAssetMenu(menuName ="Characters/Sis/Editor/Behavior Graph")]
	public class SisBehaviorGraph : BehaviorGraph<Sis>
	{
	}
}
 