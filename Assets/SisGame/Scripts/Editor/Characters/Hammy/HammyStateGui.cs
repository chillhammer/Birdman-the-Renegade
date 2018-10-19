using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;
using SIS.States;
using System.Collections.Generic;

namespace SIS.Characters.Ham
{
	[CustomEditor(typeof(HammyState))]
	public class HammyStateGUI : CustomUI.StateGUI<Hammy>
	{
	}
}
