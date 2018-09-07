using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;

namespace SIS.Characters.Example
{
	[CreateAssetMenu(menuName = "Characters/Example/Editor/Nodes/State Node")]
	public class ExampleStateNode : StateNode<Example>
	{
	}
}