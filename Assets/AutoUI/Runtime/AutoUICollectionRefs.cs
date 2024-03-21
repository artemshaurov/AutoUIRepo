using System;

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
        
        
        public string GetAssetKeyForType(Type fieldType)
        {
            if (fieldType == typeof(bool))
            {
                return "BooleanAutoNodeView";
            }
            
            if (fieldType == typeof(string))
            {
                return "StringAutoInputField";
            }
            
            if (fieldType == typeof(int))
            {
                return "IntAutoInputField";
            }
            
            if (fieldType == typeof(float))
            {
                return "FloatAutoInputField";
            }

            if (fieldType.IsEnum)
            {
                return "EnumAutoField";
            }

            return null;
        }
    }
}