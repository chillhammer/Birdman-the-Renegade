using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using SIS.States.Actions.BehaviorTree;


namespace SIS.Characters.Example
{
	//Sequence StateAction for behavior tree. Instatiated to string together other nodes
	//Sequence return success if all of their their children return success
	//Sequence will activate nodes from left to right until it fails
	[CreateAssetMenu(menuName = "Characters/Example/State Actions/Behavior Tree/Sequence")]
	public class ExampleBTSequence : ExampleBTNode
	{
		public ExampleBTNode[] bTNodes;
		AivoTree.SequenceNode<Example> sequence;

		//Copys over State Action Nodes into Sequence. State Action Nodes are acted as Tree Nodes
		public override BehaviorTreeNode<Example, ExampleStateActions> InitNode()
		{
			sequence = new AivoTree.SequenceNode<Example>(bTNodes);
			return new BehaviorTreeNode<Example, ExampleStateActions>(sequence);

		}
	}
}