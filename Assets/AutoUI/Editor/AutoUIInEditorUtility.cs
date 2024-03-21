using System.Reflection;
using Common.AutoUI.Runtime;
using UnityEditor;
using UnityEngine;

namespace Common.AutoUI.Editor
{
    [InitializeOnLoad]
    public static class AutoUIInEditorUtility
    {
        private static IAutoNode m_UISchemeForObject;
        
        static AutoUIInEditorUtility()
        {
            EditorApplication.playModeStateChanged += ModeStateChanged ;
        }
 
        private static void ModeStateChanged (PlayModeStateChange state)
        {
            switch (state)
            {
                case PlayModeStateChange.EnteredPlayMode:
                    Construct();
                    break; 
                case PlayModeStateChange.ExitingPlayMode:
                    Destruct();
                    break;
            }
        }

        private static void Construct()
        {
            EditorApplication.contextualPropertyMenu += OnContextMenuOpening;
        }

        private static void Destruct()
        {
            EditorApplication.contextualPropertyMenu -= OnContextMenuOpening;
        }

        private static void OnContextMenuOpening(GenericMenu menu, SerializedProperty property)
        {
            if (!Application.isPlaying)
            {
                return;
            }

            AddItem(menu, property);
        }

        private static void AddItem(GenericMenu menu, SerializedProperty property)
        {
            menu.AddItem(new GUIContent("Create Auto UI"), false, OnMenuClick, 
                new SerializedPropertyInfo()
            {
                PropertyPath = property.propertyPath,
                TargetObject = property.serializedObject.targetObject
            });
            
        }

        private static void OnMenuClick(object userdata)
        {
            m_UISchemeForObject?.Dispose();
            var propertyInfo = (SerializedPropertyInfo) userdata;
            
            Debug.Log("ContextMenu opening for property " + propertyInfo.TargetObject);
            var type = propertyInfo.TargetObject.GetType();

            var fieldInfo = type.GetField(propertyInfo.PropertyPath, 
                BindingFlags.Default 
                | BindingFlags.Public 
                | BindingFlags.NonPublic 
                | BindingFlags.Instance);

            if (fieldInfo == null)
            {
                return;
            }
            
            var target = fieldInfo.GetValue(propertyInfo.TargetObject);
            var autoUiCollection = new AutoUICollectionRefs();
            var autoUiController
                = new AutoUIController(
                    autoUiCollection);

            var withTag = GameObject.FindGameObjectWithTag("auto_ui_root");
            if (withTag == null)
            {
                return;
            }

            m_UISchemeForObject = autoUiController
                .CreateUISchemeForObject(
                    target, fieldInfo.FieldType, fieldInfo.Name, withTag.transform);
        }
        
    }
}