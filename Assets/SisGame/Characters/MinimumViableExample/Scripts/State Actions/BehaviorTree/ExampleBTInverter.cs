using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using SIS.States.Actions.BehaviorTree;


namespace SIS.Characters.Example
{
	//Inverter StateAction for behavior tree. Instatiated to flip the result of the given action node
	[CreateAssetMenu(menuName = "Characters/Example/State Actions/Behavior Tree/Inverter")]
	public class ExampleBTInverter : ExampleBTNode
	{
		public ExampleBTNode bTNode;
		AivoTree.InverterNode<Example> inverter;

		//Copys over State Action Nodes into Inverter. State Action Nodes are acted as Tree Nodes
		public override BehaviorTreeNode<Example, ExampleStateActions> InitNode()
		{
			inverter = new AivoTree.InverterNode<Example>(bTNode);
			return new BehaviorTreeNode<Example, ExampleStateActions>(inverter);

		}
	}
}