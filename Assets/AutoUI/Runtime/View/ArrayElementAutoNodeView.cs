using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Common.AutoUI.Runtime.View
{
    public class ArrayElementAutoNodeView : MonoBehaviour, IArrayElementAutoNodeView//, IHasResizableContent
    {
        [SerializeField] private Color m_EvenColor, m_OddColor;
        [SerializeField] private Image m_Background;
        [SerializeField] private Button m_DeleteButton;
        [SerializeField] private Transform m_InnerElementContainer;
        [SerializeField] private RectTransform m_ResizableBackRect;
        [SerializeField] private float m_MinContentSize = 100;
        private bool m_Initialized;
        private IArrayElementAutoNode m_ElementNode;
        public Transform InnerContainer => m_InnerElementContainer;
        
        public void SetLabel(string toString)
        {
            
        }

        private void OnEnable()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (m_Initialized)
            {
                return;
            }

            m_Initialized = true;
            m_DeleteButton.onClick.AddListener(OnDeleteButtonClick);
        }

        private void OnDeleteButtonClick()
        {
            m_ElementNode.RequestDeletion();
        }

        public void SetElementIsEvent(bool isEvent)
        {
            m_Background.color = isEvent ? m_EvenColor : m_OddColor;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void AssignNode(IArrayElementAutoNode elementNode)
        {
            m_ElementNode = elementNode;
        }


        private void OnTransformChildrenChanged()
        {
            StartCoroutine(LateMessage());
        }
        
        private IEnumerator LateMessage()
        {
            yield return null;
            UpdateSize();
            transform.parent.SendMessageUpwards(
                nameof(OnTransformChildrenChanged), 
                SendMessageOptions.DontRequireReceiver);
        }

        private void UpdateSize()
        {
            float contentSize = 
                Mathf.Max(
                m_MinContentSize,
                GetComponentsInChildren<IHasResizableContent>()
                    .Sum(hasResizableContent => hasResizableContent.GetContentHeight()));

            m_ResizableBackRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentSize);
        }
    }
    
    public interface IHasResizableContent
    {
        float GetContentHeight();
    }
}