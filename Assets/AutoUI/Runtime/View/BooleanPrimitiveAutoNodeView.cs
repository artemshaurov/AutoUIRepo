using UnityEngine;
using UnityEngine.UI;

namespace Common.AutoUI.Runtime.View
{
    public class BooleanPrimitiveAutoNodeView : MonoBehaviour, IPrimitiveAutoNodeView
    {
        [SerializeField] private Toggle m_Toggle;
        [SerializeField] private Text m_LabelText;
        private IPrimitiveAutoNode m_PrimitiveAutoNode;
        [SerializeField] private Transform m_LabelContainer;
        public void SetLabelStatement(bool isHidden)
        {
            if (m_LabelContainer != null)
            {
                m_LabelContainer.gameObject.SetActive(!isHidden);
            }
        }
        public void SetValue(object value)
        {
            if (value is bool boolean)
            {
                m_Toggle.SetIsOnWithoutNotify(boolean);
            }
        }

        public void SetLabel(string propertyName)
        {
            m_LabelText.text = propertyName;
        }
        
        public void AssignNode(IPrimitiveAutoNode primitiveAutoNode)
        {
            m_PrimitiveAutoNode = primitiveAutoNode;
            m_Toggle.onValueChanged.RemoveAllListeners();
            m_Toggle.onValueChanged.AddListener(OnToggleClicked);
        }

        public void ReleaseNode()
        {
            m_PrimitiveAutoNode = null;
            m_Toggle.onValueChanged.RemoveAllListeners();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnToggleClicked(bool arg0)
        {
            m_PrimitiveAutoNode.SetValue(arg0);
        }
    }
}