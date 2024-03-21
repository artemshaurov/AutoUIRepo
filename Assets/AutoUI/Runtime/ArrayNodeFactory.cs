using System;

namespace Common.AutoUI.Runtime
{
    internal class ArrayNodeFactory : IArrayNodeFactory
    {
        private readonly Type m_ElementType;
        private readonly Func<int, object, IArrayElementAutoNode> m_CreateNodeFunc;

        public ArrayNodeFactory(
            Type elementType, Func<int, object, IArrayElementAutoNode> createNodeFunc)
        {
            m_ElementType = elementType;
            m_CreateNodeFunc = createNodeFunc;
        }

        public IArrayElementAutoNode CreateElement(int index, object element)
        {
            return m_CreateNodeFunc.Invoke(index, element);
        }

        public IArrayElementAutoNode CreateNewElement(int index)
        {
            if (m_ElementType == typeof(string))
            {
                return m_CreateNodeFunc.Invoke(index, string.Empty);
            }
            return m_CreateNodeFunc.Invoke(index, Activator.CreateInstance(m_ElementType));
        }
    }
}