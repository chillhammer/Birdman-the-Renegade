using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;
using SIS.States;
using System.Collections.Generic;

namespace SIS.Characters.Robo
{
	[CustomEditor(typeof(RoboPadronState))]
	public class RoboPadronStateGUI : CustomUI.StateGUI<RoboPadron>
	{
	}
}
 