using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using SIS.States.Actions.BehaviorTree;

namespace SIS.Characters.Robo
{
	//Abstract wrapper parent node for all nodes of behavior tree
	//Allows for StateActions ScriptableObject to act as a node in behavior tree
	public abstract class RoboPadronBTNode : RoboPadronStateActions, AivoTree.TreeNode<RoboPadron>
	{
		//Node to set within Init()
		public BehaviorTreeNode<RoboPadron, RoboPadronStateActions> node;

		//Sets up Node
		private void Awake() { Init(); }
		public abstract void Init();

		//Called From State Machine. Only used if this is Root
		public override void Execute(RoboPadron owner)
		{
			node.Execute(owner);
		}

		//Called From Behavior Tree. Only used if ancestor is called from State Machine
		public AivoTree.AivoTreeStatus Tick(float timeTick, RoboPadron context)
		{
			return node.Tick(timeTick, context);
		}
	}
}