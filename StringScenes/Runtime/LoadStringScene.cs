using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading.Tasks;

public class LoadStringScene : MonoBehaviour
{
    [SerializeField]
    private string sceneName = default;

    [ContextMenu("Change scene")]
    public async void Do_ChangeScene()
    {
        await SceneTransitionManager.LoadNewScene(sceneName);
    }
}