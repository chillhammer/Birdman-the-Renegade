using UnityEngine;
using UnityEditor;
using SIS.States.Actions;
using System.Collections;
using System.Collections.Generic;
using SIS.States.Actions.BehaviorTree;

namespace SIS.Characters.Example
{
	//Parent class for all Nodes in Behavior Tree for Example enemy

	//Abstract wrapper parent node for all nodes of behavior tree
	//Allows for StateActions ScriptableObject to act as a node in behavior tree
	//	Thus, using Execute simply runs the node as if it is root.
	public abstract class ExampleBTNode : ExampleStateActions, AivoTree.TreeNode<Example>
	{
		//Lookup Map For Each Enemy
		Dictionary<Example, BehaviorTreeNode<Example, ExampleStateActions>> nodeTable;

		//Sets up Node. Assigns AivoTree node to BTNode
		public abstract BehaviorTreeNode<Example, ExampleStateActions> InitNode();

		//Called From State Machine. Only used if this is Root
		public override void Execute(Example owner)
		{
			GetNode(owner).Execute(owner);
		}

		//Called From Behavior Tree. Only used if ancestor is called from State Machine
		public AivoTree.AivoTreeStatus Tick(float timeTick, Example context)
		{
			return GetNode(context).Tick(timeTick, context);
		}

		//Allows for nodes to be reset, which means they can break out mid-way
		public void Reset(Example context)
		{
			GetNode(context).Reset(context);
		}


		//Helper to get node for this instance. Lazy instantiation
		//Since it is a Scriptable Object, we use a dictionary to have a Node for each enemy
		private BehaviorTreeNode<Example, ExampleStateActions> GetNode(Example owner)
		{
			BehaviorTreeNode<Example, ExampleStateActions> node;

			if (nodeTable == null)
				nodeTable = new Dictionary<Example, BehaviorTreeNode<Example, ExampleStateActions>>();

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
	}
}