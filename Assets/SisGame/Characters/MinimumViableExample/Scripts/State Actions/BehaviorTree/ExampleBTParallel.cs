using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using SIS.States.Actions.BehaviorTree;


namespace SIS.Characters.Example
{
	//Selector StateAction for behavior tree. Instatiated to string together other nodes
	//Parallel returns first node that is not in Running status
	//Will run all nodes concurrently

	[CreateAssetMenu(menuName = "Characters/Example/State Actions/Behavior Tree/Parallel")]
	public class ExampleBTParellel : ExampleBTNode
	{
		public ExampleBTNode[] bTNodes;
		public int successesNeeded = 1;
		public int failuresNeeded = 1;
		AivoTree.ParallelNode<Example> parellel;

		//Copys over State Action Nodes into Sequence. State Action Nodes are acted as Tree Nodes
		public override BehaviorTreeNode<Example, ExampleStateActions> InitNode()
		{
			parellel = new AivoTree.ParallelNode<Example>(bTNodes, successesNeeded, failuresNeeded);
			return new BehaviorTreeNode<Example, ExampleStateActions>(parellel);
			
		}
	}
}