using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class eventwatcher2 : MonoBehaviour
{
    //------------------------------------- async
    private void Awake()
    {
        RegisterEvent();
    }

    [ContextMenu("Register to unique's event")]
    void call()
    {
        RegisterEvent();
    }

    async void RegisterEvent()
    {
        var publisher = await Unique<Unique_TestEventPublisher2>.GetAsync();
        publisher.SomeEvent.AddListener((message) =>
        {
            Debug.Log(message + " / " + gameObject.name);
        });
    }

    [ContextMenu("Log unique instance")]
    void LogUniqueInstance()
    {
        var unique = Unique<Unique_TestEventPublisher2>.Get();
        Debug.Log(unique);
    }
}
