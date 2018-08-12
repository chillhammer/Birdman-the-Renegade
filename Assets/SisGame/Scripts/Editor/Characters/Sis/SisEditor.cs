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
			SisEditorSettings editorSettings = Resources.Load("SisEditorSettings") as SisEditorSettings;
			editorSettings.SetParentNodes();
			settings = editorSettings;
			style = settings.skin.GetStyle("window");
			activeStyle = settings.activeSkin.GetStyle("window");
		}
	}

	[CustomEditor(typeof(SisState))]
	public class SisStateGUI : CustomUI.StateGUI<Sis>
	{
	}
}
 