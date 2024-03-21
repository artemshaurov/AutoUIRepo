namespace Common.AutoUI.Runtime
{
    public interface IArrayNodeFactory
    {
        IArrayElementAutoNode CreateElement(int index, object element);
        IArrayElementAutoNode CreateNewElement(int index);
    }
}