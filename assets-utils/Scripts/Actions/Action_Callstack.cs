using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Actions
{
    public class Action_Callstack : MonoBehaviour
    {
        [SerializeField] UnityEvent Callstack = default;
        [ContextMenu("Perform callstack")]
        public void Do_Callstack()
        {
            Callstack?.Invoke();
        }
    }

}
