using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
namespace Actions
{
    public class StartExecution : MonoBehaviour
    {
        [SerializeField] UnityEvent OnStart = null;
        void Start()
        {
            OnStart?.Invoke();
        }
    }

}

