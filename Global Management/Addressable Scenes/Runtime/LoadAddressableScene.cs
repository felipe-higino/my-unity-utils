using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;

public class LoadAddressableScene : MonoBehaviour
{
    [SerializeField]
    private AssetReferenceScene sceneAddress = default;

    [ContextMenu("Change scene")]
    public async void Do_ChangeScene()
    {
        await SceneTransitionManager.LoadNewScene(sceneAddress);
    }
}