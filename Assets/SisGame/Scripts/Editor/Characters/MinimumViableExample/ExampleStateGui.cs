using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;
using SIS.States;
using System.Collections.Generic;

namespace SIS.Characters.Example
{
	[CustomEditor(typeof(ExampleState))]
	public class ExampleStateGUI : CustomUI.StateGUI<Example>
	{
	}
}
 