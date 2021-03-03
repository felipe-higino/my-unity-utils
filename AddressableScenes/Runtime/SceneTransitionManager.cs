using System.Threading.Tasks;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneTransitionManager
{
    private static List<string> scenesInBuild = default;

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

    public static async Task LoadNewScene(string sceneName)
    {
        if (IsTransitioning)
        {
            Debug.LogWarning("Transition is running, please wait for completion");
            return;
        }

        IsTransitioning = true;
        if (null != Transition)
        {
            await Transition.LowerCourtine();
            await SceneLoadAsyncByName(sceneName);
            await Transition.LiftCourtine();
        }
        else
        {
            Debug.LogWarning("Transition not found");
            await SceneLoadAsyncByName(sceneName);
        }
        IsTransitioning = false;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    private static void Init()
    {
        scenesInBuild = new List<string>();
        var sceneCount = SceneManager.sceneCountInBuildSettings;
        for (int i = 0; i < sceneCount; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            int lastSlash = scenePath.LastIndexOf("/");
            var sceneName = scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1);
            scenesInBuild.Add(sceneName);
            Debug.Log(scenePath);
        }
    }

    private static async Task SceneLoadAsyncByName(string sceneName)
    {
        if (!scenesInBuild.Contains(sceneName))
        {
            Debug.LogError("scene not in build path, returning to last scene...");
            return;
        }
        var asyncOp = SceneManager.LoadSceneAsync(sceneName);
        var task = new TaskCompletionSource<bool>();
        asyncOp.completed += (x) =>
        {
            task.SetResult(true);
        };
        await task.Task;
    }
}
