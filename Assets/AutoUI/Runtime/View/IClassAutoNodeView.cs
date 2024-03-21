using UnityEngine;

namespace Common.AutoUI.Runtime.View
{
    public interface IClassAutoNodeView : IAutoNodeView
    {
        Transform ChildRoot { get; }
        void SetLabel(string name);
    }
}