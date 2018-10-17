using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using SIS.States.Actions.BehaviorTree;


namespace SIS.Characters.Robo
{
	//Inverter StateAction for behavior tree. Instatiated to flip the result of the given action node
	[CreateAssetMenu(menuName = "Characters/RoboPadron/State Actions/Behavior Tree/Inverter")]
	public class RoboPadronBTInverter : RoboPadronBTNode
	{
		public RoboPadronBTNode bTNode;
		AivoTree.InverterNode<RoboPadron> inverter;

		//Copys over State Action Nodes into Inverter. State Action Nodes are acted as Tree Nodes
		public override BehaviorTreeNode<RoboPadron, RoboPadronStateActions> InitNode()
		{
			inverter = new AivoTree.InverterNode<RoboPadron>(bTNode);
			return new BehaviorTreeNode<RoboPadron, RoboPadronStateActions>(inverter);

		}
	}
}