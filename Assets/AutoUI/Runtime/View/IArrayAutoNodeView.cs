using UnityEngine;

namespace Common.AutoUI.Runtime.View
{
    public interface IArrayAutoNodeView
    {
        Transform ChildRoot { get; }
        void SetLabel(string name);
        void AssignNode(IArrayAutoNode node);
        void UpdateLayout();
        void Destroy();
    }
}