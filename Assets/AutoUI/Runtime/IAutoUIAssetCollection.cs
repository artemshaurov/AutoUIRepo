using System;

namespace Common.AutoUI.Runtime
{
    public interface IAutoUIAssetCollection
    {
        string GetRootClassAutoNodeUIReference();
        string GetInnerClassAutoNodeUIReference();
        string GetArrayAutoNodeUIReference();
        string GetArrayElementAutoNodeUIReference();
        string GetAssetKeyForType(Type fieldType);
    }
}