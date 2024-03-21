namespace Common.AutoUI.Runtime
{
    public interface IArrayElementAutoNode : IAutoNode
    {
        IAutoNode Inner { get; }
        int Index { get;  }
        void SetIndex(int index);
        void Delete();
        void RequestDeletion();
    }
}