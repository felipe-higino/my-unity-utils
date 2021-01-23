using UnityEngine;

namespace Unique.tests
{
    internal class MyUniqueTest : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log($"{gameObject.name}");
            Unique<MyUniqueTest>.Generate(this);
        }

        [ContextMenu("instance name")]
        void printtest()
        {
            var obj = Unique<MyUniqueTest>.Get();
            Debug.Log($"unique: {obj.name}");
        }
    }

}