using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using SIS.States.Actions.BehaviorTree;


namespace SIS.Characters.Robo
{
	//Sequence StateAction for behavior tree. Instatiated to string together other nodes
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Selector")]
	public class RoboPadronBTSelector : RoboPadronBTNode
	{
		public RoboPadronBTNode[] bTNodes;
		AivoTree.SelectorNode<RoboPadron> selector;

		//Copys over State Action Nodes into Sequence. State Action Nodes are acted as Tree Nodes
		public override BehaviorTreeNode<RoboPadron, RoboPadronStateActions> InitNode()
		{
			selector = new AivoTree.SelectorNode<RoboPadron>(bTNodes);
			return new BehaviorTreeNode<RoboPadron, RoboPadronStateActions>(selector);
			
		}
	}
}