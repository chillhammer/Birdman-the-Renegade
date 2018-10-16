using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using SIS.States.Actions.BehaviorTree;


namespace SIS.Characters.Robo
{
	//Selector StateAction for behavior tree. Instatiated to string together other nodes
	//Parallel returns first node that is not in Running status
	//Will run all nodes concurrently

	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Parllel")]
	public class ExampleBTParellel : RoboPadronBTNode
	{
		public RoboPadronBTNode[] bTNodes;
		AivoTree.ParellelNode<RoboPadron> parellel;

		//Copys over State Action Nodes into Sequence. State Action Nodes are acted as Tree Nodes
		public override BehaviorTreeNode<RoboPadron, RoboPadronStateActions> InitNode()
		{
			parellel = new AivoTree.ParellelNode<RoboPadron>(bTNodes);
			return new BehaviorTreeNode<RoboPadron, RoboPadronStateActions>(parellel);
			
		}
	}
}