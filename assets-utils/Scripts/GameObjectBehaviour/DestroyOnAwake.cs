using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actions
{
    [DisallowMultipleComponent]
    public class DestroyOnAwake : MonoBehaviour
    {
        private void Awake()
        {
            Destroy(gameObject);
        }
    }

}
