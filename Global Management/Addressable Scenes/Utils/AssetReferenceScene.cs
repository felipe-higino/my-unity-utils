using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine.AddressableAssets
{
    /// <summary>
    /// Wrapper for a Asset Addressable of type SceneAsset / *.unity
    /// </summary>
    [Serializable]
    public class AssetReferenceScene

#if UNITY_EDITOR
    : AssetReferenceT<SceneAsset>
#else
    : AssetReference
#endif

    {
        public AssetReferenceScene(string guid) : base(guid)
        {
        }
    }
}