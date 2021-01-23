using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class PauseGame : MonoBehaviour
{
    [SerializeField] UnityEvent OnPause = default;
    [SerializeField] UnityEvent OnUnpause = default;

    IEnumerable<IPausable> pausables = default;

    static bool isPaused = false;

    void Start()
    {
        pausables = FindObjectsOfType<MonoBehaviour>().OfType<IPausable>();
    }

    public void Pause()
    {
        isPaused = true;
        foreach (var pausable in pausables)
            pausable.OnPause();

        OnPause?.Invoke();
    }

    public void Unpause()
    {
        isPaused = false;
        foreach (var pausable in pausables)
            pausable.OnUnpause();

        OnUnpause?.Invoke();
    }

    public void TogglePause()
    {
        if (isPaused)
            Unpause();
        else
            Pause();
    }
}
