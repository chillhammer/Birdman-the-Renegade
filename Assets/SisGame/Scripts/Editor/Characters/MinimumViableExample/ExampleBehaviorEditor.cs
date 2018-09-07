using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;
using SIS.States;
using System.Collections.Generic;

namespace SIS.Characters.Example
{
	public class SisBehaviorEditor : BehaviorEditor<Example>
	{
		[MenuItem("Behavior Editor/Example Editor")]
		static void ShowEditor()
		{
			editor = EditorWindow.GetWindow<SisBehaviorEditor>();
			editor.minSize = new Vector2(800, 600);
		}

		protected override void LoadEditorSettings()
		{
			// !!
			//Make sure to create ExampleEditorSettings in Resources folder inside character folder!!
			// !!
			ExampleEditorSettings editorSettings = Resources.Load("ExampleEditorSettings") as ExampleEditorSettings;
			editorSettings.SetParentNodes();
			settings = editorSettings;
		}
	}
}
 