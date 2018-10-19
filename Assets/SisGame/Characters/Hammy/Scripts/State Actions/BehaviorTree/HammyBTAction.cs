using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using SIS.States.Actions.BehaviorTree;

//General actions, found in SisGame/Scripts/States/State Actions
//	can be inherited to get some baseline functionality
//	must be in it it's own file to get avoid ScriptableObject corruption

namespace SIS.Characters.Ham
{
	//Abstract Parent action for all leaf nodes of behavior tree
	[System.Serializable]
	public abstract class HammyBTAction : HammyBTNode
	{
		AivoTree.ActionNode<Hammy> actionNode;

		public override BehaviorTreeNode<Hammy, HammyStateActions> InitNode()
		{
			actionNode = new AivoTree.ActionNode<Hammy>(Act);
			return new BehaviorTreeNode<Hammy, HammyStateActions>(actionNode);
		}

		public abstract AivoTree.AivoTreeStatus Act(float timeTick, Hammy owner);
	}
}