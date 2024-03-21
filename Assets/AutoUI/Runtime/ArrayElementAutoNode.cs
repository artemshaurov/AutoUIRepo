using Common.AutoUI.Runtime.View;

namespace Common.AutoUI.Runtime
{
    public class ArrayElementAutoNode : IArrayElementAutoNode
    {
        private IArrayElementAutoNodeView m_NodeView;
        private IArrayAutoNode m_ArrayAutoNode;
        public IAutoNode Inner { get; private set; }
        public int Index { get; private set; }
        
        public string Name 
            => Inner.Name;

        public void SetName(string name) => Inner.SetName(name);

        public void SetInner(IAutoNode inner)
        {
            Inner = inner;
            if (Inner is IPrimitiveAutoNode primitiveAutoNode)
            {
                primitiveAutoNode.TurnOffLabel();
            }
        }

        public void SetIndex(int index)
        {
            Index = index;
            m_NodeView?.SetElementIsEvent(Index % 2 == 0);
            Inner.SetName(index.ToString());
        }

        public void BindTo(IArrayElementAutoNodeView nodeView)
        {
            m_NodeView = nodeView;
            m_NodeView.AssignNode(this);
        }

        public void SetRootArray(IArrayAutoNode arrayAutoNode)
        {
            m_ArrayAutoNode = arrayAutoNode;
        }

        public void Delete()
        {
            m_NodeView.Destroy();
            m_NodeView = null;
        }

        public void RequestDeletion()
        {
            m_ArrayAutoNode.RequestDeletion(this);
        }

        public void Dispose()
        {
            Inner?.Dispose();
        }
    }
}