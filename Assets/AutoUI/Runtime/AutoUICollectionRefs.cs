using System;
using System.Collections.Generic;

namespace Common.AutoUI.Runtime
{
    public class AutoUICollectionRefs : IAutoUIAssetCollection
    {
        public string GetRootClassAutoNodeUIReference()
            => "RootClassAutoNodeView";

        public string GetInnerClassAutoNodeUIReference()
            => "InnerClassAutoNodeView";

        public string GetArrayAutoNodeUIReference()
            => "ArrayAutoNodeView";

        public string GetArrayElementAutoNodeUIReference() 
            => "ArrayElementAutoNode";

        private Dictionary<Type, string> m_PrimitiveTypeToAssetKey = new Dictionary<Type, string>()
        {
            {typeof(bool), "BooleanAutoNodeView" },
            {typeof(string), "StringAutoInputField" },
            {typeof(int), "IntAutoInputField" },
            {typeof(float), "FloatAutoInputField" }
        };
        
        
        public string GetAssetKeyForType(Type fieldType)
        {
            if (m_PrimitiveTypeToAssetKey.ContainsKey(fieldType))
            {
                return m_PrimitiveTypeToAssetKey[fieldType];
            }

            if (fieldType.IsEnum)
            {
                return "EnumAutoField";
            }

            return null;
        }
    }
}