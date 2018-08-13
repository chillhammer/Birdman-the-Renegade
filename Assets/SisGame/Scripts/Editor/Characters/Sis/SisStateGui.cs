using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;
using SIS.States;
using System.Collections.Generic;

namespace SIS.Characters.Sis
{
	[CustomEditor(typeof(SisState))]
	public class SisStateGUI : CustomUI.StateGUI<Sis>
	{
	}
}
 