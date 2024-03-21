namespace Common.AutoUI.Runtime
{
    public interface IArrayAutoNode : IRootAutoNode
    {
        void CreateNewElement();
        void RequestDeletion(IArrayElementAutoNode elementNode);
    }
}