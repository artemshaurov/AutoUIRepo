namespace Common.AutoUI.Runtime.View
{
    public interface IPrimitiveAutoNodeView
    {
        void SetValue(object value);
        void SetLabel(string propertyName);
        void SetLabelStatement(bool isHidden);
        void AssignNode(IPrimitiveAutoNode primitiveAutoNode);
        void ReleaseNode();
        void Destroy();
    }
}