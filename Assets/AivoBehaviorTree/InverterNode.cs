namespace AivoTree
{
    public class InverterNode<T> : TreeNode<T>
    {
        private readonly TreeNode<T> node;

        public InverterNode(TreeNode<T> node)
        {
            this.node = node;
        }
        
        public AivoTreeStatus Tick(long timeTick, T context)
        {
            var status = node.Tick(timeTick, context);
            switch (status)
            {
                case AivoTreeStatus.Success:
                    return AivoTreeStatus.Failure;
                case AivoTreeStatus.Failure:
                    return AivoTreeStatus.Success;
                default:
                    return status;
            }
        }
    }
}