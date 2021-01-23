using UnityEngine;

namespace Unique.tests
{
    internal class UniqueMonitor : MonoBehaviour
    {
        [ContextMenu("Check class")]
        void pinttest()
        {
            var obj = Unique<MyUniqueTest>.Get();
            Debug.Log(obj);
        }
    }
}