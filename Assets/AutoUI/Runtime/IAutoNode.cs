using System;

namespace Common.AutoUI.Runtime
{
    public interface IAutoNode : IDisposable
    {
        string Name { get; }
        void SetName(string name);
    }
}