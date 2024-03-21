using Common.AutoUI.Runtime;
using UnityEngine;

namespace Common.AutoUI.Samples
{
    public class AutoUITest : MonoBehaviour
    {
        [SerializeField] private Transform m_Root;
        [SerializeField] private TestClass m_TestClass;
        [SerializeField] private TestClass m_Deserialized;
        private AutoUIController m_AutoUIController;
        private IAutoNode m_RootNode;

        private void Create()
        {
            var autoUIController
                = new AutoUIController(new AutoUICollectionRefs());

            m_AutoUIController = autoUIController;
            
            m_RootNode = m_AutoUIController
                .CreateUISchemeForObject(m_TestClass, "UI Test", m_Root);
        }

        private void OnGUI()
        {
            if (GUI.Button(new Rect(20, 300, 200, 100), "Create"))
            {
                Create();
            }
            
            if (GUI.Button(new Rect(240, 300, 200, 100), "Deserialize"))
            {
                m_Deserialized = AutoUIController.Deserialize<TestClass>(m_RootNode);
            }
        }
    }
}