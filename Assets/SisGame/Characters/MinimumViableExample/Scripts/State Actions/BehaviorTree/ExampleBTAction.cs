using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using SIS.States.Actions.BehaviorTree;

//General actions, found in SisGame/Scripts/States/State Actions
//	can be inherited to get some baseline functionality
//	must be in it it's own file to get avoid ScriptableObject corruption

namespace SIS.Characters.Example
{
	//Abstract Parent action for all leaf nodes of behavior tree
	[System.Serializable]
	public abstract class ExampleBTAction : ExampleBTNode
	{
		AivoTree.ActionNode<Example> actionNode;

		public override BehaviorTreeNode<Example, ExampleStateActions> InitNode()
		{
			actionNode = new AivoTree.ActionNode<Example>(Act);
			return new BehaviorTreeNode<Example, ExampleStateActions>(actionNode);
		}

		public abstract AivoTree.AivoTreeStatus Act(float timeTick, Example owner);
	}
}