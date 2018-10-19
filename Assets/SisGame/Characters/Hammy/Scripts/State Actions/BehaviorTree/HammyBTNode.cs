using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using System.Collections;
using System.Collections.Generic;
using SIS.States.Actions.BehaviorTree;

namespace SIS.Characters.Ham
{
	//Parent class for all Nodes in Behavior Tree for Hammy enemy

	//Abstract wrapper parent node for all nodes of behavior tree
	//Allows for StateActions ScriptableObject to act as a node in behavior tree
	//	Thus, using Execute simply runs the node as if it is root.
	public abstract class HammyBTNode : HammyStateActions, AivoTree.TreeNode<Hammy>
	{
		//Lookup Map For Each Enemy
		Dictionary<Hammy, BehaviorTreeNode<Hammy, HammyStateActions>> nodeTable;

		//Sets up Node. Assigns AivoTree node to BTNode
		public abstract BehaviorTreeNode<Hammy, HammyStateActions> InitNode();

		//Called From State Machine. Only used if this is Root
		public override void Execute(Hammy owner)
		{
			GetNode(owner).Execute(owner);
		}

		//Called From Behavior Tree. Only used if ancestor is called from State Machine
		public AivoTree.AivoTreeStatus Tick(float timeTick, Hammy context)
		{
			return GetNode(context).Tick(timeTick, context);
		}


		//Helper to get node for this instance. Lazy instantiation
		//Since it is a Scriptable Object, we use a dictionary to have a Node for each enemy
		private BehaviorTreeNode<Hammy, HammyStateActions> GetNode(Hammy owner)
		{
			BehaviorTreeNode<Hammy, HammyStateActions> node;

			if (nodeTable == null)
				nodeTable = new Dictionary<Hammy, BehaviorTreeNode<Hammy, HammyStateActions>>();

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

		public void Reset(Hammy owner)
		{
			//GetNode(owner).Reset(owner);
		}
	}
}