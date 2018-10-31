using System;

namespace AivoTree
{
    public class ActionNode<T> : TreeNode<T>
    {
        private readonly Func<float, T, AivoTreeStatus> _fn;

        public ActionNode(Func<float, T, AivoTreeStatus> fn)
        {
            _fn = fn;
        }
        
        public AivoTreeStatus Tick(float timeTick, T context)
        {
            return _fn(timeTick, context);
        }

		public void Reset(T context)
		{
		}
    }
}