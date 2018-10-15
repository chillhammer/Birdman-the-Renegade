using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using SIS.States.Actions.BehaviorTree;


namespace SIS.Characters.Example
{
	//Selector StateAction for behavior tree. Instatiated to string together other nodes
	//Selectors return successful if at least one of their children return successful
	//Selectors will activate nodes from left to right until it reaches successful node.
	//	At failure or success, Selectors will try again the next step. Good for roots
	[CreateAssetMenu(menuName = "Characters/Example/State Actions/Behavior Tree/Selector")]
	public class ExampleBTSelector : ExampleBTNode
	{
		public ExampleBTNode[] bTNodes;
		AivoTree.SelectorNode<Example> selector;

		//Copys over State Action Nodes into Sequence. State Action Nodes are acted as Tree Nodes
		public override BehaviorTreeNode<Example, ExampleStateActions> InitNode()
		{
			selector = new AivoTree.SelectorNode<Example>(bTNodes);
			return new BehaviorTreeNode<Example, ExampleStateActions>(selector);
			
		}
	}
}