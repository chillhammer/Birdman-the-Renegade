namespace AivoTree
{
    public interface TreeNode<T>
    {
        AivoTreeStatus Tick(float timeTick, T context);
		void Reset(T context);
    }
}