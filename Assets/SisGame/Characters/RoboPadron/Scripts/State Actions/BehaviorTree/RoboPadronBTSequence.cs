using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using SIS.States.Actions.BehaviorTree;


namespace SIS.Characters.Robo
{
	//Sequence StateAction for behavior tree. Instatiated to string together other nodes
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Sequence")]
	public class RoboPadronBTSequence : RoboPadronBTNode
	{
		public RoboPadronBTNode[] bTNodes;
		AivoTree.SequenceNode<RoboPadron> sequence;

		//Copys over State Action Nodes into Sequence. State Action Nodes are acted as Tree Nodes
		public override void Init()
		{
			sequence = new AivoTree.SequenceNode<RoboPadron>(bTNodes); //Actually create node
			node = new BehaviorTreeNode<RoboPadron, RoboPadronStateActions>(sequence); //Saves as Wrapper
		}
	}
}