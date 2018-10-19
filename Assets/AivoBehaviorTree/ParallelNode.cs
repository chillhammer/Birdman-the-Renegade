using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AivoTree
{
    public class ParallelNode<T> : TreeNode<T>
    {
        private readonly TreeNode<T>[] _nodes;
		private int sucessesNeeded;
		private int failuresNeeded;
		private int sucesses;
		private int failures;

        public ParallelNode(TreeNode<T>[] nodes, int sucessesNeeded, int failuresNeeded)
        {
            _nodes = nodes;
			this.sucessesNeeded = sucessesNeeded;
			this.failuresNeeded = failuresNeeded;
			sucesses = 0;
			failures = 0;
        }

		public AivoTreeStatus Tick(float timeTick, T context)
		{
			var nodesToSearch = _nodes;
			int i = 0;
			sucesses = 0;
			failures = 0;
			foreach (TreeNode<T> node in nodesToSearch)
			{
				AivoTreeStatus status = node.Tick(timeTick, context);
				if (status == AivoTreeStatus.Failure)
				{
					++failures;
				}
				if (status == AivoTreeStatus.Success)
				{
					++sucesses;
				}
				++i;

				//Termination Conditions
				if (sucesses >= sucessesNeeded)
				{
					//UnityEngine.Debug.Log("Parallel Node Succeeded. Successes: " + sucesses + " and Failures: " + failures + ". Successes Needed: " + sucessesNeeded);
					Reset(context);
					return AivoTreeStatus.Success;
				}
				if (failures >= failuresNeeded)
				{
					//UnityEngine.Debug.Log("Parallel Node Failed. Successes: " + sucesses + " and Failures: " + failures);
					Reset(context);
					return AivoTreeStatus.Failure;
				}
			}
			
			//UnityEngine.Debug.Log("Parallel Node is Running. Successes: " + sucesses + " and Failures: " + failures + ". Successes Needed: " + sucessesNeeded);
			return AivoTreeStatus.Running;
		}

		public void Reset(T context)
		{
			ResetTerminators();
			foreach (TreeNode<T> node in _nodes)
			{
				node.Reset(context);
			}
		}

		private void ResetTerminators()
		{
			sucesses = 0;
			failures = 0;
		}
	}
}