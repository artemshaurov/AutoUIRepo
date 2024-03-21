using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Common.AutoUI.Runtime.View
{
    public class ArrayAutoNodeView : MonoBehaviour, IArrayAutoNodeView 
    {
        [SerializeField] private Text m_LabelText;
        [SerializeField] private Transform m_FoldoutIconContainer;
        [SerializeField] private RectTransform m_BackRect;
        [SerializeField] private ScrollRect m_ScrollView;
        [SerializeField] private float m_HiddenSize, m_MaxExpandedSize;
        [SerializeField] private Button m_FoldoutButton;
        [SerializeField] private bool m_Expanded;
        [SerializeField] private Button m_PlusButton;
        [SerializeField] private bool m_AlwaysExpandFull = false;
        private bool m_Initialized;
        private IArrayAutoNode m_ArrayAutoNode;

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
            m_FoldoutButton.onClick.AddListener(ChangeExpandedStatement);
            m_PlusButton.onClick.AddListener(OnPlusButtonClick);

            ExpandedStatementChange();
        }

        public void AssignNode(IArrayAutoNode node)
        {
            m_ArrayAutoNode = node;
        }

        public void UpdateLayout()
        {
            OnTransformChildrenChanged();
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnPlusButtonClick()
        {
            m_ArrayAutoNode.CreateNewElement();
        }

        private void ChangeExpandedStatement()
        {
            m_Expanded = !m_Expanded;
            ExpandedStatementChange();
        }

        private void ExpandedStatementChange()
        {
            m_FoldoutIconContainer.eulerAngles
                = m_Expanded
                    ? Vector3.back * 90
                    : Vector3.zero;
            
            m_ScrollView.content.gameObject.SetActive(m_Expanded);
            
            UpdateScrollRect();
            OnTransformChildrenChanged();
        }

        private void UpdateScrollRect()
        {
            var fullExpandedHeight = m_ScrollView.content.rect.height + m_HiddenSize;
            float targetHeight = fullExpandedHeight;
            bool scrollEnabled = false;
            if (!m_AlwaysExpandFull)
            {
                targetHeight = Mathf.Min(fullExpandedHeight, m_MaxExpandedSize);  
                scrollEnabled = fullExpandedHeight > m_MaxExpandedSize;
                if (!scrollEnabled)
                {
                    m_ScrollView.velocity = Vector2.zero;
                    m_ScrollView.verticalNormalizedPosition = 1;
                }
            }

            m_ScrollView.enabled = scrollEnabled;
            m_BackRect.SetSizeWithCurrentAnchors(
                RectTransform.Axis.Vertical,
                m_Expanded ? targetHeight : m_HiddenSize);
        }

        private void OnTransformChildrenChanged() => StartCoroutine(LateMessage());

        private IEnumerator LateMessage()
        {
            yield return null;
            UpdateScrollRect();
            
            transform.parent.SendMessageUpwards(
                nameof(OnTransformChildrenChanged), 
                SendMessageOptions.DontRequireReceiver);
        }


        public void SetLabel(string label) => m_LabelText.text = label;

        public Transform ChildRoot => m_ScrollView.content;
        
    }
}