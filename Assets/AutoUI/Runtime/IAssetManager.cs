using UnityEngine;

namespace Common.AutoUI.Runtime
{
    public interface IAssetManager
    {
        GameObject Instantiate(string assetKey, Transform parent);
    }
}