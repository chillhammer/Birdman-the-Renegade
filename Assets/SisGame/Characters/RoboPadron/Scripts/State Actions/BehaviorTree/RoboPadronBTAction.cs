using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using SIS.States.Actions.BehaviorTree;

//General actions, found in SisGame/Scripts/States/State Actions
//	can be inherited to get some baseline functionality
//	must be in it it's own file to get avoid ScriptableObject corruption

namespace SIS.Characters.Robo
{
	//Abstract Parent action for all leaf nodes of behavior tree
	[System.Serializable]
	public abstract class RoboPadronBTAction : RoboPadronBTNode
	{
		AivoTree.ActionNode<RoboPadron> actionNode;

		public override BehaviorTreeNode<RoboPadron, RoboPadronStateActions> InitNode()
		{
			actionNode = new AivoTree.ActionNode<RoboPadron>(Act);
			return new BehaviorTreeNode<RoboPadron, RoboPadronStateActions>(actionNode);
		}

		public abstract AivoTree.AivoTreeStatus Act(float timeTick, RoboPadron owner);
	}
}