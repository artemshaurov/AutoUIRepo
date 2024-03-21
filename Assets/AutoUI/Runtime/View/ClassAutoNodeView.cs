using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Common.AutoUI.Runtime.View
{
    public class ClassAutoNodeView :  MonoBehaviour, IClassAutoNodeView, IHasResizableContent
    {
        [SerializeField] private Text m_LabelText;
        [SerializeField] private Transform m_FoldoutIconContainer;
        [SerializeField] private RectTransform m_BackRect;
        [SerializeField] private ScrollRect m_ScrollView;
        [SerializeField] private float m_HiddenSize, m_MaxExpandedSize;
        [SerializeField] private Button m_FoldoutButton;
        [SerializeField] private bool m_Expanded;
        [SerializeField] private bool m_AlwaysExpandFull = false;
        
        private bool m_Initialized;
        private float m_ContentHeight;
        
        public Transform ChildRoot => m_ScrollView.content;

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

            ExpandedStatementChange();
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
            m_ContentHeight = fullExpandedHeight;
            bool scrollEnabled = false;
            if (!m_AlwaysExpandFull)
            {
                m_ContentHeight = Mathf.Min(fullExpandedHeight, m_MaxExpandedSize);  
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
                m_Expanded ? m_ContentHeight : m_HiddenSize);
        }

        private void OnTransformChildrenChanged()
        {
            StartCoroutine(LateMessage());
        }

        private IEnumerator LateMessage()
        {
            yield return null;
            UpdateScrollRect();
            transform.parent.SendMessageUpwards(
                nameof(OnTransformChildrenChanged), 
                SendMessageOptions.DontRequireReceiver);
        }
        
        public void SetLabel(string label)
        {
            m_LabelText.text = label;
        }

        public float GetContentHeight()
        {
            return m_Expanded ? m_ContentHeight : m_HiddenSize;
        }

        public void Destroy()
        {
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
    }
}