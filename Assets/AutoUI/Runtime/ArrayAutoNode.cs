using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Common.AutoUI.Runtime.View;

namespace Common.AutoUI.Runtime
{
    public class ArrayAutoNode : IArrayAutoNode
    {
        private readonly List<IArrayElementAutoNode> m_Children;
        private IArrayNodeFactory m_ArrayNodeFactory;
        private IArrayAutoNodeView m_NodeView;
        private object m_TargetObject;
        public string Name { get; private set; }

        public Type FieldType { get; }

        public ArrayAutoNode(Type classType, string name)
        {
            Name = name;
            m_Children = new List<IArrayElementAutoNode>();
            FieldType = classType;
        }

        public void SetName(string name)
        {
            Name = name;
            m_NodeView?.SetLabel(name);
        }

        public void BindTo(object target)
        {
            m_TargetObject = target;
            var type = m_TargetObject.GetType();
            var fieldInfo = type.GetField(Name, BindingFlags.Public
                                                | BindingFlags.Instance
                                                | BindingFlags.Default
                                                | BindingFlags.NonPublic);
        }

        public object CreateDefault()
        {
            var children = GetChildren();
            var length = children.Count;
            if (FieldType.IsArray)
            {
                return Array.CreateInstance(
                    FieldType.GetElementType() ?? throw new InvalidOperationException(), length);
            }

            return Activator.CreateInstance(FieldType);
        }

        public IList<IAutoNode> GetChildren()
        {
            return new List<IAutoNode>(m_Children.Select(c => c.Inner));
        }

        public void SetView(IArrayAutoNodeView nodeView)
        {
            m_NodeView = nodeView;
            m_NodeView.SetLabel(Name);
            m_NodeView.AssignNode(this);
        }

        public void DropView()
        {
            m_NodeView = null;
        }
        
        public void AddChild(IArrayElementAutoNode node)
        {
            m_Children.Add(node);
        }

        public void CreateNewElement()
        {
            var autoNode = m_ArrayNodeFactory.CreateNewElement(m_Children.Count);
            m_Children.Add(autoNode);
            m_NodeView.UpdateLayout();
        }

        public void RequestDeletion(IArrayElementAutoNode elementNode)
        {
            var index = elementNode.Index;
            elementNode.Delete();
            m_Children.RemoveAt(index);
            
            for (var i = 0; i < m_Children.Count; i++)
            {
                m_Children[i].SetIndex(i);
            }
            
            m_NodeView.UpdateLayout();
        }

        public void AddNodeFactory(IArrayNodeFactory arrayNodeFactory)
        {
            m_ArrayNodeFactory = arrayNodeFactory;
        }

        public void Dispose()
        {
            m_NodeView?.Destroy();
        }
    }
}