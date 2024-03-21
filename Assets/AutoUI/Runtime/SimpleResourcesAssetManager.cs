using UnityEngine;

namespace Common.AutoUI.Runtime
{
    public class SimpleResourcesAssetManager : IAssetManager
    {
        public GameObject Instantiate(string assetKey, Transform parent)
        {
            var prefab = Resources.Load<GameObject>(assetKey);
            return Object.Instantiate(prefab, parent);
        }
    }
}