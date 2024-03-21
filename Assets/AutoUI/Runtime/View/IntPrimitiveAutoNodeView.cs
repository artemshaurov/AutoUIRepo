using UnityEngine;
using UnityEngine.UI;

namespace Common.AutoUI.Runtime.View
{
    public class IntPrimitiveAutoNodeView : MonoBehaviour, IPrimitiveAutoNodeView
    {
        [SerializeField] private InputField m_InputField;
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
            if (value is int number)
            {
                m_InputField.text = number.ToString();
            }
        }

        public void SetLabel(string propertyName)
        {
            m_LabelText.text = propertyName;
        }

       
        public void AssignNode(IPrimitiveAutoNode primitiveAutoNode)
        {
            m_PrimitiveAutoNode = primitiveAutoNode;
            m_InputField.contentType = InputField.ContentType.IntegerNumber;
            m_InputField.onValueChanged.RemoveAllListeners();
            m_InputField.onEndEdit.AddListener(OnEndEdit);
        }

        public void ReleaseNode()
        {
            m_PrimitiveAutoNode = null;
            m_InputField.onEndEdit.RemoveAllListeners();
        }

        private void OnEndEdit(string text)
        {
            if (int.TryParse(text, out var number))
            {
                m_PrimitiveAutoNode.SetValue(number);
            }
        }
        
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}