using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;

namespace SIS.Characters.Example
{
	[CreateAssetMenu(menuName = "Characters/Example/Editor/Nodes/Transition Node")]
	public class ExampleTransitionNode : TransitionNode<Example>
	{
	}
}