using System.Reflection;
using Common.AutoUI.Runtime.View;

namespace Common.AutoUI.Runtime
{
    public class PrimitiveAutoNode : IPrimitiveAutoNode 
    {
        private object m_Value;
        private IPrimitiveAutoNodeView m_BondView;
        private object m_Target;
        private FieldInfo m_FieldInfo;

        public string Name { get; private set; }

        public void SetView(IPrimitiveAutoNodeView view)
        {
            DropView();

            view.AssignNode(this);
            view.SetValue(m_Value);
            view.SetLabel(Name);

            m_BondView = view;
        }

        public void DropView()
        {
            m_BondView?.ReleaseNode();
            m_BondView = null;
        }

        public void TurnOffLabel()
        {
            m_BondView.SetLabelStatement(true);
        }

        public void SetValue(object args)
        {
            m_Value = args;
            UpdateBondedObjectValue();
        }

        public object GetValue()
        {
            return m_Value;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        private void UpdateBondedObjectValue()
        {
            if (m_FieldInfo != null && m_Target != null)
            {
                m_FieldInfo.SetValue(m_Target, m_Value);
            }
        }

        public void BindTo(object target)
        {
            m_Target = target;
            var type = target.GetType();
            m_FieldInfo = type.GetField(Name);
            UpdateBondedObjectValue();
        }

        public void Dispose()
        {
            m_BondView?.Destroy();
        }
    }
}