using UnityEngine;

namespace Common.AutoUI.Runtime.View
{
    public interface IArrayElementAutoNodeView
    {
        void AssignNode(IArrayElementAutoNode elementNode);
        void SetElementIsEvent(bool elementType);
        void Destroy();
        Transform InnerContainer { get; }
    }
}