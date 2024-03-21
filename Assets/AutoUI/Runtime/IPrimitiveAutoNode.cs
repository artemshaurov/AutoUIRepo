namespace Common.AutoUI.Runtime
{
    public interface IPrimitiveAutoNode : IAutoNode
    {
        void SetValue(object args);
        object GetValue();
        void TurnOffLabel();
    }
}