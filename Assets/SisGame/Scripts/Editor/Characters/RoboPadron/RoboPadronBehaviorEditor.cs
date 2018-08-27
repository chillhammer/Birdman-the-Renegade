using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;
using SIS.States;
using System.Collections.Generic;

namespace SIS.Characters.Robo
{
	public class RoboPadronBehaviorEditor : BehaviorEditor<RoboPadron>
	{
		[MenuItem("Behavior Editor/RoboPadron Editor")]
		static void ShowEditor()
		{
			editor = EditorWindow.GetWindow<RoboPadronBehaviorEditor>();
			editor.minSize = new Vector2(800, 600);
		}

		protected override void LoadEditorSettings()
		{
			RoboPadronEditorSettings editorSettings = Resources.Load("RoboPadronEditorSettings") as RoboPadronEditorSettings;
			editorSettings.SetParentNodes();
			settings = editorSettings;
		}
	}
}
 