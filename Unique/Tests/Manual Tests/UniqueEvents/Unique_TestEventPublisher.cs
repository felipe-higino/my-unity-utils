using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class Unique_TestEventPublisher : MonoBehaviour
{
    [Serializable] public class MyEvent : UnityEvent<string> { };
    [HideInInspector] public MyEvent SomeEvent = default;

    private void Awake()
    {
        Unique<Unique_TestEventPublisher>.Generate(this);
    }

    [ContextMenu("Emmit event")]
    void EmmitEvent()
    {
        SomeEvent?.Invoke("retornei isto do evento do tipo 1");
    }
}
