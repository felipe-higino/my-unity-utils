using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    public static T Instance;
    protected void AwakeSingleton(T objectRef)
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = objectRef;
    }
}