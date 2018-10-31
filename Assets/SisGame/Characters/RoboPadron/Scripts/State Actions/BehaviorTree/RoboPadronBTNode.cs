using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using System.Collections;
using System.Collections.Generic;
using SIS.States.Actions.BehaviorTree;

namespace SIS.Characters.Robo
{
	//Abstract wrapper parent node for all nodes of behavior tree
	//Allows for StateActions ScriptableObject to act as a node in behavior tree
	public abstract class RoboPadronBTNode : RoboPadronStateActions, AivoTree.TreeNode<RoboPadron>
	{
		//Lookup Map For Each Enemy
		Dictionary<RoboPadron, BehaviorTreeNode<RoboPadron, RoboPadronStateActions>> nodeTable;

		//Sets up Node
		public abstract BehaviorTreeNode<RoboPadron, RoboPadronStateActions> InitNode();

		//Called From State Machine. Only used if this is Root
		public override void Execute(RoboPadron owner)
		{
			
			GetNode(owner).Execute(owner);
		}

		//Called From Behavior Tree. Only used if ancestor is called from State Machine
		public AivoTree.AivoTreeStatus Tick(float timeTick, RoboPadron context)
		{
			return GetNode(context).Tick(timeTick, context);
		}


		//Helper to get node for this instance. Lazy instantiation
		private BehaviorTreeNode<RoboPadron, RoboPadronStateActions> GetNode(RoboPadron owner)
		{
			BehaviorTreeNode<RoboPadron, RoboPadronStateActions> node;

			if (nodeTable == null)
				nodeTable = new Dictionary<RoboPadron, BehaviorTreeNode<RoboPadron, RoboPadronStateActions>>();

			if (!nodeTable.ContainsKey(owner))
			{
				node = InitNode();
				nodeTable.Add(owner, node);
			}
			else
			{
				nodeTable.TryGetValue(owner, out node);
			}
			return node;
		}
		public void Reset(RoboPadron owner)
		{
			GetNode(owner).Reset(owner);
		}
	}

}