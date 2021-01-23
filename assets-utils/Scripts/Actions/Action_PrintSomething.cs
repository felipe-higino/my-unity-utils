using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actions
{
    public class Action_PrintSomething : MonoBehaviour
    {
        [SerializeField] string message = default;
        public void Do_PrintSomething()
        {
            Debug.Log(message);
        }
    }

}
