using UnityEngine;
using UnityEngine.Events;
using System.Threading.Tasks;

public static class Unique<T> where T : MonoBehaviour
{
    private static T Instance { set; get; } = null;
    private static TaskCompletionSource<T> GeneratedComplete = new TaskCompletionSource<T>();

    public static void Generate(T Component)
    {
        if (null == Component)
            return;

        if (null == Instance && GeneratedComplete.Task.IsCompleted)
            GeneratedComplete = new TaskCompletionSource<T>();

        if (null == Instance)
        {
            Instance = Component;
            if (!GeneratedComplete.Task.IsCompleted)
                GeneratedComplete.SetResult(Component);
        }

        if (Instance == Component)
            return;

        if (null != Instance)
            Object.Destroy(Component.gameObject);
    }

    public static T Get()
    {
        return Instance;
    }

    public static Task<T> GetAsync()
    {
        return GeneratedComplete.Task;
    }
}