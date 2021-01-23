// using System.Collections;
// using System.Collections.Generic;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;

// namespace Tests
// {
//     internal class Unique_1 : MonoBehaviour
//     {
//         public int value = 0;
//         private void Awake()
//         {
//             Unique<Unique_1>.Generate(this);
//         }
//     }

//     public class UniqueFactoryTests
//     {
//         GameObject obj1 = new GameObject();
//         GameObject obj2 = new GameObject();

//         [Test]
//         public void UniqueGet()
//         {
//             var uniqueInstance = obj1.AddComponent<Unique_1>();
//             uniqueInstance.value = 1;

//             var uniqueGet = Unique<Unique_1>.Get();
//             Assert.AreEqual(uniqueGet.value, uniqueInstance.value);
//         }
//     }
// }