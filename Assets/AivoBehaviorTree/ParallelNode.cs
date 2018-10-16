using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace AivoTree
{
    public class ParallelNode<T> : TreeNode<T>
    {
        private readonly TreeNode<T>[] _nodes;
		private HashSet<int> indicesToSkip;
		private int sucessesNeeded;
		private int failuresNeeded;
		private int sucesses;
		private int failures;

        public ParallelNode(TreeNode<T>[] nodes, int sucessesNeeded, int failuresNeeded)
        {
            _nodes = nodes;
			this.sucessesNeeded = sucessesNeeded;
			this.failuresNeeded = failuresNeeded;
			indicesToSkip = new HashSet<int>();
			sucesses = 0;
			failures = 0;
        }

		public AivoTreeStatus Tick(float timeTick, T context)
		{
			var nodesToSearch = _nodes;
			int i = 0;
			foreach (TreeNode<T> node in nodesToSearch)
			{
				if (indicesToSkip.Contains(i))
				{
					++i;
					continue;
				}
				AivoTreeStatus status = node.Tick(timeTick, context);
				if (status == AivoTreeStatus.Failure)
				{
					indicesToSkip.Add(i);
					++failures;
				}
				if (status == AivoTreeStatus.Success)
				{
					indicesToSkip.Add(i);
					++sucesses;
				}
			}
			if (sucesses >= sucessesNeeded)
			{
				Reset();
				return AivoTreeStatus.Success;
			}
			if (failures >= failuresNeeded || indicesToSkip.Count >= nodesToSearch.Length)
			{
				Reset();
				return AivoTreeStatus.Failure;
			}

			return AivoTreeStatus.Running;
		}

		private void Reset()
		{
			indicesToSkip.Clear();
			sucesses = 0;
			failures = 0;
		}
	}
}