using System;

namespace AivoTree
{
    public class ActionNode<T> : TreeNode<T>
    {
        private readonly Func<long, T, AivoTreeStatus> _fn;

        public ActionNode(Func<long, T, AivoTreeStatus> fn)
        {
            _fn = fn;
        }
        
        public AivoTreeStatus Tick(long timeTick, T context)
        {
            return _fn(timeTick, context);
        }
    }
}