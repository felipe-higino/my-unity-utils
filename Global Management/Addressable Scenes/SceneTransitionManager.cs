using System.Threading.Tasks;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class SceneTransitionManager
{
    private static List<ISceneTransition> _transition = new List<ISceneTransition>();
    public static ISceneTransition Transition
    {
        get => _transition.FirstOrDefault();
        set
        {
            if (null == value)
            {
                Debug.LogError("Can't assign a null transition");
                return;
            }
            _transition.Add(value);
        }
    }

    public static bool IsTransitioning { get; private set; } = false;

    public static async Task LoadNewScene(AssetReferenceScene sceneAddress)
    {
        if (null == sceneAddress)
        {
            Debug.LogError("Required scene is invalid");
            return;
        }

        if (IsTransitioning)
        {
            Debug.LogWarning("Transition is running, please wait for completion");
            return;
        }

        IsTransitioning = true;
        if (null != Transition)
        {
            await Transition.LowerCourtine();
            await sceneAddress.LoadSceneAsync().Task;
            await Transition.LiftCourtine();
        }
        else
        {
            Debug.LogWarning("Transition not found");
            await sceneAddress.LoadSceneAsync().Task;
        }
        IsTransitioning = false;
    }
}
