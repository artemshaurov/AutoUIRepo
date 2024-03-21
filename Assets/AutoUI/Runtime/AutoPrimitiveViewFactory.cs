using Common.AutoUI.Runtime.View;
using UnityEngine;

namespace Common.AutoUI.Runtime
{
    public class AutoPrimitiveViewFactory
    {
        private readonly IAssetManager m_AssetManage;

        public AutoPrimitiveViewFactory(IAssetManager assetManage)
        {
            m_AssetManage = assetManage;
        }
        
        public IPrimitiveAutoNodeView CreatePrimitive(
            string sampleKey, 
            Transform parent, 
            string gameObjectName)
        {
            var gameObject = m_AssetManage.Instantiate(sampleKey, parent);
            gameObject.name = gameObjectName;
            return gameObject.GetComponent<IPrimitiveAutoNodeView>();
        }
        
        public IClassAutoNodeView CreateClass(
            string sampleKey, 
            Transform parent, 
            string gameObjectName)
        {
            var gameObject = m_AssetManage.Instantiate(sampleKey, parent);
            gameObject.name = gameObjectName;
            return gameObject.GetComponent<IClassAutoNodeView>();
        }

        public IArrayAutoNodeView CreateArray(
            string objectKey, Transform parent, string gameObjectName)
        {
            var gameObject = m_AssetManage.Instantiate(objectKey, parent);
            gameObject.name = gameObjectName;
            return gameObject.GetComponent<IArrayAutoNodeView>();
        }
        
        public IArrayElementAutoNodeView CreateArrayElement(
            string objectKey, Transform parent, string gameObjectName)
        {
            var gameObject = m_AssetManage.Instantiate(objectKey, parent);
            gameObject.name = gameObjectName;
            return gameObject.GetComponent<IArrayElementAutoNodeView>();
        }
    }
}