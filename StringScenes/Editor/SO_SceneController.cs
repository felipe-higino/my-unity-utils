using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scene Controller UI", menuName = "Felipe Utils/Scene Controller", order = 50)]
public class SO_SceneController : ScriptableObject
{
    [SerializeField]
    private string nameOfScene = default;

    public async void LoadSelectedScene()
    {
        if (!Application.isPlaying)
        {
            Debug.LogError("Just in play mode");
            return;
        }

        await SceneTransitionManager.LoadNewScene(nameOfScene);
    }
}
