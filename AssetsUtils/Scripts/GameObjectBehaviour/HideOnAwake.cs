using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actions
{
    [DisallowMultipleComponent]
    public class HideOnAwake : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(false);
        }
    }

}
