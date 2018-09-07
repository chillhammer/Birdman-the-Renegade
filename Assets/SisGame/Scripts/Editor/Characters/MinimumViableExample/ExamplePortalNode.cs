using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;

namespace SIS.Characters.Example
{
	[CreateAssetMenu(menuName = "Characters/Example/Editor/Nodes/Portal Node")]
	public class ExamplePortalNode : PortalNode<Example>
	{
	}
}