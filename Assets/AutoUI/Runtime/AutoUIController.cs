using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Common.AutoUI.Runtime
{
    public class AutoUIController
    {
        private readonly IAutoUIAssetCollection m_AssetCollection;
        private readonly AutoPrimitiveViewFactory m_AutoUIFactory;

        private int m_Depth;
        public AutoUIController(IAutoUIAssetCollection assetCollection)
        {
            m_AssetCollection = assetCollection;
            IAssetManager assetManager = new SimpleResourcesAssetManager();
            m_AutoUIFactory = new AutoPrimitiveViewFactory(assetManager);
        }

        public IAutoNode CreateUISchemeForObject(object target, string name, Transform root)
        {
            return CreateUISchemeForObject(target, target.GetType(), name, root);
        }

        public IAutoNode CreateUISchemeForObject(object target, Type targetType, string name, Transform root)
        {
            IncreaseDepth();
            if (targetType == typeof(string) || targetType.IsPrimitive || targetType.IsEnum)
            {
                DecreaseDepth();
                return CreatePrimitiveNode(target, targetType, name, root);
            }

            if (target is ICollection)
            {
                DecreaseDepth();
                return CreateArray(root, name, target);
            }

            var rootNode = new ClassAutoNode(targetType, name);
            var classObjectKey 
                = m_Depth == 1 
                    ? m_AssetCollection.GetRootClassAutoNodeUIReference()
                    : m_AssetCollection.GetInnerClassAutoNodeUIReference();
            
            if (classObjectKey == null)
            {
                throw new Exception();
            }

            var classAutoNodeView
                = m_AutoUIFactory.CreateClass(classObjectKey, root, name);

            rootNode.SetView(classAutoNodeView);
            rootNode.SetName(name);

            var childRoot = classAutoNodeView.ChildRoot;

            var fieldInfos = targetType.GetFields(
                BindingFlags.Public
                | BindingFlags.Instance
                | BindingFlags.Default
                | BindingFlags.NonPublic);
            
            foreach (var fieldInfo in fieldInfos)
            {
                object fieldValue = fieldInfo.GetValue(target);
                var fieldType = fieldInfo.FieldType;
                if (fieldValue == null && !(fieldType == typeof(string) || fieldType.IsPrimitive || fieldType.IsEnum))
                {
                    fieldValue = CreateDefaultTarget(fieldType);
                }

                var node = CreateUISchemeForObject(fieldValue, fieldType, fieldInfo.Name, childRoot);
                if (node == null)
                {
                    continue;
                }

                rootNode.AddChild(node);
            }

            IncreaseDepth();
            return rootNode;
        }

        private object CreateDefaultTarget(Type targetType)
        {
            return CreateInstance(targetType);
        }

        private static object CreateInstance(Type type, int? arraySize = null)
        {
            return type.IsArray 
                ? Activator.CreateInstance(
                type, 
                arraySize.GetValueOrDefault(0))
                : Activator.CreateInstance(type);
        }

        private IAutoNode CreateArray(Transform root, string name, object target)
        {
            var targetType = target.GetType();
            
            var rootNode = new ArrayAutoNode(targetType, name);
            var objectKey = m_AssetCollection.GetArrayAutoNodeUIReference();
            if (objectKey == null)
            {
                throw new Exception();
            }
            
            var arrayAutoNodeView
                = m_AutoUIFactory.CreateArray(objectKey, root, name);

            rootNode.SetView(arrayAutoNodeView);

            var collection = (ICollection) target;

            var elementType = targetType.IsGenericType 
                ? targetType.GetGenericArguments()[0] 
                : collection.GetType().GetElementType();

            if (elementType == null)
            {
                throw new Exception();
            }

            var arrayNodeFactory = new ArrayNodeFactory(
                elementType,
                (index, element) 
                    => CreateArrayElementAutoNode(element, elementType, index, arrayAutoNodeView.ChildRoot, rootNode));

            rootNode.AddNodeFactory(arrayNodeFactory);

            int i = 0;
            foreach (var listElement in collection)
            {
                var autoNode = arrayNodeFactory.CreateElement(i, listElement);
                rootNode.AddChild(autoNode);
                i++;
            }

            return rootNode;
        }

        private IArrayElementAutoNode CreateArrayElementAutoNode(
            object target, 
            Type targetType, 
            int index,
            Transform root,
            IArrayAutoNode arrayAutoNode)
        {
            var rootNode
                = new ArrayElementAutoNode();
            
            var objectKey = m_AssetCollection.GetArrayElementAutoNodeUIReference();
            if (objectKey == null)
            {
                throw new Exception();
            }
            
            var elementView
                = m_AutoUIFactory.CreateArrayElement(
                    objectKey, root, index.ToString());

            
            rootNode.BindTo(elementView);

            var innerNode = CreateUISchemeForObject(
                target, targetType, index.ToString(), elementView.InnerContainer);
            
            rootNode.SetInner(innerNode);
            rootNode.SetIndex(index);
            rootNode.SetRootArray(arrayAutoNode);
            return rootNode;
        }

        private IAutoNode CreatePrimitiveNode(
            object target, Type targetType, string fieldName, Transform root)
        {
            var assetKey = m_AssetCollection.GetAssetKeyForType(targetType);
            if (assetKey == null)
            {
                return null;
            }

            var node = new PrimitiveAutoNode();
            var autoPrimitiveView = m_AutoUIFactory.CreatePrimitive(assetKey, root, fieldName);

            node.SetName(fieldName);
            node.SetValue(target);
            node.SetView(autoPrimitiveView);
            return node;
        }

        private void IncreaseDepth()
        {
            m_Depth++;
        }

        private void DecreaseDepth()
        {
            m_Depth--;
        }

        public static T Deserialize<T>(IAutoNode classAutoNode)
        {
            return (T) DeserializeNode(classAutoNode);
        }

        private static object DeserializeNode(IAutoNode node)
        {
            return node switch
            {
                IPrimitiveAutoNode primitiveAutoNode => primitiveAutoNode.GetValue(),
                IClassAutoNode classAutoNode => DeserializeClassNode(classAutoNode),
                IArrayAutoNode arrayAutoNode => DeserializeArrayNode(arrayAutoNode),
                _ => null
            };
        }

        private static object DeserializeArrayNode(IArrayAutoNode arrayAutoNode)
        {
            var children = arrayAutoNode.GetChildren();
            var length = children.Count;
            object inst;
            if (arrayAutoNode.FieldType.IsArray)
            {
                inst = Array.CreateInstance(
                    arrayAutoNode.FieldType.GetElementType() ?? throw new InvalidOperationException(), length);
            }
            else
            {
                inst = Activator.CreateInstance(arrayAutoNode.FieldType);
            }

            switch (inst)
            {
                case Array array:
                {
                    for (int i = 0; i < length; i++)
                    {
                        var childObjValue = DeserializeNode(children[i]);
                        array.SetValue(childObjValue, i);
                    }

                    break;
                }
                case IList list:
                {
                    foreach (var child in children)
                    {
                        var childObjValue = DeserializeNode(child);
                        list.Add(childObjValue);
                    }

                    break;
                }
            }

            return inst;
        }

        private static object DeserializeClassNode(IClassAutoNode classAutoNode)
        {
            var inst = Activator.CreateInstance(classAutoNode.FieldType);
            var fieldInfos = classAutoNode.FieldType.GetFields(
                    BindingFlags.Public
                    | BindingFlags.Instance
                    | BindingFlags.Default
                    | BindingFlags.NonPublic)
                .ToDictionary(f => f.Name, f => f);


            foreach (var childNode in classAutoNode.GetChildren())
            {
                var childObjValue = DeserializeNode(childNode);
                fieldInfos[childNode.Name].SetValue(inst, childObjValue);
            }

            return inst;
        }
    }
}