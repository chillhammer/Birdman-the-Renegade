using System;
using System.Linq;

namespace AivoTree
{
    public class SequenceNode<T> : TreeNode<T>
    {
        private readonly TreeNode<T>[] _nodes;
        private TreeNode<T> runningNode;

        public SequenceNode(params TreeNode<T>[] nodes)
        {
            _nodes = nodes;
        }

        public AivoTreeStatus Tick(float timeTick, T context)
        {
            var nodesToSearch = runningNode == null
                ? _nodes
                : _nodes.SkipWhile(node => node != runningNode);
            return nodesToSearch.Aggregate(AivoTreeStatus.Success, (acc, curr) =>
            {
                if (acc == AivoTreeStatus.Success)
                {
                    var result = curr.Tick(timeTick, context);
                    if (result == AivoTreeStatus.Running)
                    {
                        runningNode = curr;
                    }
                    else
                    {
                        runningNode = null;
                    }
                    return result;
                }
                return acc;
            });
        }

		public void Reset(T context)
		{
			foreach (TreeNode<T> node in _nodes)
			{
				node.Reset(context);
				runningNode = null;
			}
		}
	}
}