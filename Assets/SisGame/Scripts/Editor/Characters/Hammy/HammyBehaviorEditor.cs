using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;
using SIS.States;
using System.Collections.Generic;

namespace SIS.Characters.Ham
{
	public class HammyBehaviorEditor : BehaviorEditor<Hammy>
	{
		[MenuItem("Behavior Editor/Hammy Editor")]
		static void ShowEditor()
		{
			editor = EditorWindow.GetWindow<HammyBehaviorEditor>();
			editor.minSize = new Vector2(800, 600);
		}

		protected override void LoadEditorSettings()
		{
			// !!
			//Make sure to create ExampleEditorSettings in Resources folder inside character folder!!
			// !!
			HammyEditorSettings editorSettings = Resources.Load("HammyEditorSettings") as HammyEditorSettings;
			editorSettings.SetParentNodes();
			settings = editorSettings;
		}
	}
}
