using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using SIS.States.Actions.BehaviorTree;


namespace SIS.Characters.Ham
{
	//Selector StateAction for behavior tree. Instatiated to string together other nodes
	//Parallel returns first node that is not in Running status
	//Will run all nodes concurrently

	[CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Behavior Tree/Parallel")]
	public class HammyBTParallel : HammyBTNode
	{
		public HammyBTNode[] bTNodes;
		public int successesNeeded = 1;
		public int failuresNeeded = 1;
		AivoTree.ParallelNode<Hammy> parellel;

		//Copys over State Action Nodes into Sequence. State Action Nodes are acted as Tree Nodes
		public override BehaviorTreeNode<Hammy, HammyStateActions> InitNode()
		{
			parellel = new AivoTree.ParallelNode<Hammy>(bTNodes, successesNeeded, failuresNeeded);
			return new BehaviorTreeNode<Hammy, HammyStateActions>(parellel);

		}
	}
}