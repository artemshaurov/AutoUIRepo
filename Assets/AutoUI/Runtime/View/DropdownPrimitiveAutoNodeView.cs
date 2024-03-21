using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Common.AutoUI.Runtime.View
{
    public class DropdownPrimitiveAutoNodeView : MonoBehaviour, IPrimitiveAutoNodeView, IPointerClickHandler
    {
        [SerializeField] private Dropdown m_Dropdown;
        [SerializeField] private Text m_LabelText;
        private IPrimitiveAutoNode m_PrimitiveAutoNode;
        [SerializeField] private Transform m_LabelContainer;
        private Array m_EnumValues;

        public void SetLabelStatement(bool isHidden)
        {
            if (m_LabelContainer != null)
            {
                m_LabelContainer.gameObject.SetActive(!isHidden);
            }
        }
        public void SetValue(object value)
        {
            var type = value.GetType();
            if (!type.IsEnum)
            {
                return;
            }

            m_Dropdown.SetValueWithoutNotify((int)value);
        }

        public void SetLabel(string propertyName)
        {
            m_LabelText.text = propertyName;
        }
        public void AssignNode(IPrimitiveAutoNode primitiveAutoNode)
        {
            m_PrimitiveAutoNode = primitiveAutoNode;
            m_Dropdown.ClearOptions();
            var type = primitiveAutoNode.GetValue().GetType();
            var enumNames = Enum.GetNames(type);
            m_EnumValues = Enum.GetValues(type);
            
            m_Dropdown.AddOptions(enumNames.ToList());
            m_Dropdown.onValueChanged.AddListener(OnDropdownSelectionChanged);
        }

        private void OnDropdownSelectionChanged(int dropdown)
        {
            m_PrimitiveAutoNode.SetValue(dropdown);
        }

        public void ReleaseNode()
        {
            m_PrimitiveAutoNode = null;
            m_Dropdown.ClearOptions();
            m_Dropdown.onValueChanged.RemoveAllListeners();
        }
        
        
        public void Destroy()
        {
            Destroy(gameObject);
        }

        // public void OnPointerClick(PointerEventData eventData)
        // {
        //     var i = m_Dropdown.value;
        //     m_Dropdown.Show();
        //
        //     var scroll = m_Dropdown.GetComponentInChildren<ScrollRect>();
        //     var contentRect = scroll.content.rect;
        //
        //     var singleElementHeight
        //         = contentRect.height / m_EnumValues.Length;
        //
        //     LogAllStaff(scroll, i);
        //     var offset = singleElementHeight * i;
        //     
        //     scroll.content.anchoredPosition
        //         = new Vector2(0, offset);
        // }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            var i = m_Dropdown.value;
            m_Dropdown.Show();
        
            var scroll = m_Dropdown.GetComponentInChildren<ScrollRect>();
            var contentRect = scroll.content.rect;
            var viewFraction = scroll.viewport.rect.height / contentRect.height;

            if (viewFraction >= 1)
            {
                scroll.verticalNormalizedPosition = 1;
                return;
            }

            scroll.verticalNormalizedPosition
                = 1 - 1f / ((1f - viewFraction) * m_EnumValues.Length) * i;
        }
    }
}