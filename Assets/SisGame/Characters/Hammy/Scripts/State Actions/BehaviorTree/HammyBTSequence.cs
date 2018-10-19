using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using SIS.States.Actions.BehaviorTree;


namespace SIS.Characters.Ham
{
	//Sequence StateAction for behavior tree. Instatiated to string together other nodes
	//Sequence return success if all of their their children return success
	//Sequence will activate nodes from left to right until it fails
	[CreateAssetMenu(menuName = "Characters/Hammy/State Actions/Behavior Tree/Sequence")]
	public class HammyBTSequence : HammyBTNode
	{
		public HammyBTNode[] bTNodes;
		AivoTree.SequenceNode<Hammy> sequence;

		//Copys over State Action Nodes into Sequence. State Action Nodes are acted as Tree Nodes
		public override BehaviorTreeNode<Hammy, HammyStateActions> InitNode()
		{
			sequence = new AivoTree.SequenceNode<Hammy>(bTNodes);
			return new BehaviorTreeNode<Hammy, HammyStateActions>(sequence);

		}
	}
}