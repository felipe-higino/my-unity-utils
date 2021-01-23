using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Actions
{
    public class OnSetActiveExecution : MonoBehaviour
    {
        public UnityEvent OnSetActive;
        private void OnEnable()
        {
            OnSetActive?.Invoke();
        }
    }
}