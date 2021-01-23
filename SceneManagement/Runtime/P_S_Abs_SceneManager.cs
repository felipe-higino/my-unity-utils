using AnimationEvents;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;


public abstract class P_S_Abs_SceneManager<EnumScenes> :
    Singleton<P_S_Abs_SceneManager<EnumScenes>>
    where EnumScenes : Enum
{
    [Header("References injection")]
    [SerializeField] Animator SceneTransition = null;

    [Header("Parameters:")]
    [SerializeField] List<EnumScenes> ProhibitedScenes = null;
    [SerializeField] EnumScenes LoadingScene = default;


    public event Action On_LoadingScene_StartLoading;
    public event Action<EnumScenes> On_StagedScene_StartLoading;
    public event Action<EnumScenes> On_StagedScene_CourtineFullyUp;
    public event Action On_Courtine_FullyDown;
    public event Action<EnumScenes> On_ActualScene_Quit;

    EnumScenes activeScene = default;
    public EnumScenes ActiveScene { get => activeScene; }

    private void Awake()
    {
        //persistence
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    ///		Normally called by <see cref="LoadOtherScene"/> monobehaviour component
    /// </summary>
    public void ChangeScene(EnumScenes scene)
    {
        if (ProhibitedScenes.Contains(scene))
        {
            Debug.LogError("This scene is prohibited to be loaded");
            return;
        }

        On_ActualScene_Quit?.Invoke(activeScene);
        ShowLoadingScene(() =>
        {
            ShowStagedScene(scene);
        });
    }


    #region Loading scene and staged scene ------------------------------------------------

    async void ShowLoadingScene(Action OnLoadingSceneFullyShown)
    {
        On_LoadingScene_StartLoading?.Invoke();
        //courtine for loading scene
        SceneTransition.SetTrigger("fadeOut");
        await T_WaitForCourtineFullyDown();

        Unique<Unique_AudioManager>.Get()?.StopAllAudioSources();
        var loadingScene90Loaded = await T_Load90SceneAsync(LoadingScene);

        await T_Load100SceneAsync(loadingScene90Loaded);
        SceneTransition.SetTrigger("fadeIn");

        await T_WaitForCourtineFullyUp();
        OnLoadingSceneFullyShown?.Invoke();
    }

    async void ShowStagedScene(EnumScenes StagedScene)
    {
        On_StagedScene_StartLoading?.Invoke(StagedScene);

        var scene90Loaded = await T_Load90SceneAsync(StagedScene);
        await Task.Delay(1000);

        SceneTransition.SetTrigger("fadeOut");
        await T_WaitForCourtineFullyDown();

        await T_Load100SceneAsync(scene90Loaded);
        SceneTransition.SetTrigger("fadeIn");

        await T_WaitForCourtineFullyUp();
        On_StagedScene_CourtineFullyUp?.Invoke(StagedScene);
        activeScene = StagedScene;
    }

    #endregion ---------------------------------------------------------------------------


    #region Animation waiters TASK -------------------------------------------------------

    Task<bool> T_WaitForCourtineFullyUp()
    {
        var TaskSolver = new TaskCompletionSource<bool>();

        Action OnCourtineFullyHide = null;
        OnCourtineFullyHide = () =>
        {
            TaskSolver?.TrySetResult(true);
            //self-removed callback
            AnimationEvent_FadeInEnd.Action_OnFadeInEnd -= OnCourtineFullyHide;
        };
        AnimationEvent_FadeInEnd.Action_OnFadeInEnd += OnCourtineFullyHide;

        return TaskSolver.Task;
    }

    Task<bool> T_WaitForCourtineFullyDown()
    {
        var TaskSolver = new TaskCompletionSource<bool>();

        Action OnCourtineFullyShown = null;
        OnCourtineFullyShown = () =>
        {
            On_Courtine_FullyDown?.Invoke();
            TaskSolver?.TrySetResult(true);
            //self-removed callback
            AnimationEvent_FadeOutEnd.Action_OnFadeOutEnd -= OnCourtineFullyShown;
        };
        AnimationEvent_FadeOutEnd.Action_OnFadeOutEnd += OnCourtineFullyShown;

        return TaskSolver.Task;
    }

    #endregion --------------------------------------------------------------------------


    #region Scene loaders ---------------------------------------------------------------

    Task<AsyncOperation> T_Load90SceneAsync(EnumScenes scene)
    {
        var TaskSolver = new TaskCompletionSource<AsyncOperation>();
        StartCoroutine(_LoadSceneCoroutine(scene, (loadOp) =>
        {
            TaskSolver?.TrySetResult(loadOp);
        }));
        return TaskSolver.Task;
    }

    Task<bool> T_Load100SceneAsync(AsyncOperation scene90loaded)
    {
        var TaskSolver = new TaskCompletionSource<bool>();
        scene90loaded.allowSceneActivation = true;
        scene90loaded.completed += (_) =>
        {
            TaskSolver?.TrySetResult(true);
        };
        return TaskSolver.Task;
    }

    IEnumerator _LoadSceneCoroutine(EnumScenes scene, Action<AsyncOperation> sceneAlmostLoaded)
    {
        var index = Convert.ToInt32(scene);

        var loading = SceneManager.LoadSceneAsync(index);
        loading.allowSceneActivation = false;

        //start loading
        var loadPercentage = 0f;
        while (loadPercentage < .9f)
        {
            loadPercentage = loading.progress;
            yield return null;
        }

        sceneAlmostLoaded?.Invoke(loading);
    }


    #endregion --------------------------------------------------------------------------
}
