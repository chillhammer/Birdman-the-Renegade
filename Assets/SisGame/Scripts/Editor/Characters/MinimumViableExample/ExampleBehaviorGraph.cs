using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;

namespace SIS.Characters.Example
{
	[CreateAssetMenu(menuName = "Characters/Example/Editor/Behavior Graph")]
	public class ExampleBehaviorGraph : BehaviorGraph<Example>
	{
	}
}