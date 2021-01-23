using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Actions
{
    public class AwakeExecution : MonoBehaviour
    {
        [SerializeField] UnityEvent onAwake = default;

        private void Awake()
        {
            onAwake?.Invoke();
        }
    }

}
