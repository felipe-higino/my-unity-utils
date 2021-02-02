using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Scene Controller UI", menuName = "Felipe Utils/Scene Controller", order = 50)]
public class SO_SceneController : ScriptableObject
{
    [SerializeField]
    private AssetReferenceScene SceneToLoad = default;

    public async void LoadSelectedScene()
    {
        if (!Application.isPlaying)
        {
            Debug.LogError("Just in play mode");
            return;
        }

        await SceneTransitionManager.LoadNewScene(SceneToLoad);
    }
}
