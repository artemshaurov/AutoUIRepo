using System;
using System.Collections.Generic;
using Common.AutoUI.Runtime.View;
using UnityEngine;

namespace Common.AutoUI.Runtime
{
    public class ClassAutoNode : IClassAutoNode
    {
        private readonly List<IAutoNode> m_Children;
        private IClassAutoNodeView m_ClassNodeView;
        public string Name { get; private set; }
        public Type FieldType { get; }

        public ClassAutoNode(Type classType, string name)
        {
            m_Children = new List<IAutoNode>();
            Name = name;
            FieldType = classType;
            SetName(name);
        }

        public void SetName(string name)
        {
            Name = name;
            m_ClassNodeView?.SetLabel(name);
        }

        public object CreateDefault()
        {
            return Activator.CreateInstance(FieldType);
        }

        public IList<IAutoNode> GetChildren()
        {
            return m_Children;
        }

        public void AddChild(IAutoNode node)
        {
            m_Children.Add(node);
        }

        public void SetView(IClassAutoNodeView classNode)
        {
            m_ClassNodeView = classNode;
            m_ClassNodeView.SetLabel(Name);
        }

        public void Dispose()
        {
            if (m_ClassNodeView is MonoBehaviour monoBehaviour
                && monoBehaviour != null)
            {
                m_ClassNodeView?.Destroy();
            }
        }
    }
}