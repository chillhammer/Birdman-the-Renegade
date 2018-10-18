using UnityEngine;
using System.Collections;
using SIS.Characters;
using AivoTree;

namespace SIS.States.Actions.BehaviorTree
{
	//Wrapper class for TreeNode
	//Allows for State Actions encapsulate tree node specific to character

	public class BehaviorTreeNode<C, SA> where C : Character
									where SA : StateActions<C>
	{
		private TreeNode<C> node;

		//Constructor to pass in existing node
		public BehaviorTreeNode(TreeNode<C> passedInNode) {
			node = passedInNode;
		}

		//Allows for StateActions children to call this as root
		public void Execute(C owner)
		{
			Tick(owner.delta, owner);
		}

		//To be called by parent node
		public AivoTreeStatus Tick(float timeTick, C characterContext)
		{
			return node.Tick(timeTick, characterContext);
		}

		public void Reset(C characterContext)
		{
			node.Reset(characterContext);
		}
		
	}
}