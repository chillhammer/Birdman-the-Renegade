using UnityEngine;
using UnityEditor;
using SIS.BehaviorEditor;

namespace SIS.Characters.Example
{
	[CreateAssetMenu(menuName = "Characters/Example/Editor/Nodes/Comment Node")]
	public class ExampleCommentNode : CommentNode<Example>
	{
	}
}