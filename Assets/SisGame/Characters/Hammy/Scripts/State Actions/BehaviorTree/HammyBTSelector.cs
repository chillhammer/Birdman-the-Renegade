using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using SIS.States.Actions.BehaviorTree;


namespace SIS.Characters.Ham
{
	//Selector StateAction for behavior tree. Instatiated to string together other nodes
	//Selectors return successful if at least one of their children return successful
	//Selectors will activate nodes from left to right until it reaches successful node.
	//	At failure or success, Selectors will try again the next step. Good for roots
	[CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Behavior Tree/Selector")]
	public class HammyBTSelector : HammyBTNode
	{
		public HammyBTNode[] bTNodes;
		AivoTree.SelectorNode<Hammy> selector;

		//Copys over State Action Nodes into Sequence. State Action Nodes are acted as Tree Nodes
		public override BehaviorTreeNode<Hammy, HammyStateActions> InitNode()
		{
			selector = new AivoTree.SelectorNode<Hammy>(bTNodes);
			return new BehaviorTreeNode<Hammy, HammyStateActions>(selector);

		}
	}
}