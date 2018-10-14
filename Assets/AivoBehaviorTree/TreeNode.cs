namespace AivoTree
{
    public interface TreeNode<T>
    {
        AivoTreeStatus Tick(long timeTick, T context);
    }
}