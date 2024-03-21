using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Common.AutoUI.Runtime
{
    public class SimpleAddressablesAssetManager : IAssetManager
    {
        public GameObject Instantiate(string assetKey, Transform parent)
        {
            var op 
                = Addressables.InstantiateAsync(assetKey, parent);

            return op.WaitForCompletion();
        }
    }
}