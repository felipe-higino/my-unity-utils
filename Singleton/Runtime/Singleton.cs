using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                var founded = FindObjectsOfType<T>();
                if (founded.Length > 0)
                {
                    instance = founded[0];
                    for (var i = 1; i < founded.Length - 1; i++)
                        Destroy(founded[i].gameObject);
                }
            }
            return instance;
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }
}