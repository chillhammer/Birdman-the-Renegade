using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;
using SIS.States;
using System.Collections.Generic;

namespace SIS.Characters.Sis
{
	public class SisBehaviorEditor : BehaviorEditor<Sis>
	{
		new SisBaseNode selectedNode;

		//public new SisEditorSettings settings;
		protected new static SisBehaviorEditor editor;
		new static SisState previousState;

		[MenuItem("Behavior Editor/Sis Editor")]
		static void ShowEditor()
		{
			editor = EditorWindow.GetWindow<SisBehaviorEditor>();
			editor.minSize = new Vector2(800, 600);
		}

		protected override void OnEnable()
		{
			Debug.Log("Attempting to find Settings");
			BehaviorEditor<Sis>.settings = Resources.Load("EditorSettings") as SisEditorSettings;
			style = settings.skin.GetStyle("window");
			activeStyle = settings.activeSkin.GetStyle("window");

			if (settings == null)
				Debug.Log("Settings is null!");
			else
				Debug.Log("Settings is " + settings.GetType().Name);
		}

		protected override void DrawWindows()
		{
			GUILayout.BeginArea(all, style);

			BeginWindows();
			EditorGUILayout.LabelField(" ", GUILayout.Width(100));
			EditorGUILayout.LabelField("Assign Graph:", GUILayout.Width(100));
			settings.currentGraph = (SisBehaviorGraph)EditorGUILayout.ObjectField(settings.currentGraph, typeof(SisBehaviorGraph), false, GUILayout.Width(200));

			if (settings.currentGraph != null)
			{
				foreach (SisBaseNode n in settings.currentGraph.windows)
				{
					n.DrawCurve();
				}

				for (int i = 0; i < settings.currentGraph.windows.Count; i++)
				{
					BaseNode<Sis> b = settings.currentGraph.windows[i];

					Debug.Log(b.GetType().Name);

					if (b.drawNode is Characters.Sis.SisStateNode)
						Debug.Log("drawing SIS state node");

					if (b.drawNode is SisStateNode)
					{
						Debug.Log("drawing state node");
						if (currentStateManager != null && b.stateRef.currentState == currentStateManager.currentState)
						{
							b.windowRect = GUI.Window(i, b.windowRect,
								DrawNodeWindow, b.windowTitle, activeStyle);
						}
						else
						{
							b.windowRect = GUI.Window(i, b.windowRect,
								DrawNodeWindow, b.windowTitle);
						}
					}
					else
					{
						b.windowRect = GUI.Window(i, b.windowRect,
							DrawNodeWindow, b.windowTitle);
					}
				}
			}
			EndWindows();

			GUILayout.EndArea();


		}
	}

	[CreateAssetMenu(menuName = "Characters/Sis/Editor/Settings")]
	public class SisEditorSettings : EditorSettings<Sis>
	{
		public new SisBehaviorGraph currentGraph;
		public new SisStateNode stateNode;
		public new SisPortalNode portalNode;
		public new SisTransitionNode transitionNode;
		public new SisCommentNode commentNode;
	}

	[System.Serializable]
	public class SisBaseNode : BaseNode<Sis>
	{
		[SerializeField]
		public new SisStateNodeReferences stateRef;
		[SerializeField]
		public new SisTransitionNodeReferences transRef;
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
		public new void DrawWindow(BaseNode<Sis> b)
		{
			Debug.Log("Drawing new State Node Window");
		}
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
		[SerializeField]
		public new List<SisBaseNode> windows = new List<SisBaseNode>();
	}

	[System.Serializable]
	public class SisStateNodeReferences: StateNodeReferences<Sis>
	{
		public new SisState currentState;
		[HideInInspector]
		public new SisState previousState;
	}

	[System.Serializable]
	public class SisTransitionNodeReferences : TransitionNodeReferences<Sis>
	{
		[HideInInspector]
		public new SisCondition previousCondition;
	}
}
 