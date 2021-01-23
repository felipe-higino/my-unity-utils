

// using System.Collections;
// using System.Collections.Generic;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;

// namespace Tests
// {
//     internal enum TagsA { A_ENUM_1, A_ENUM_2 }
//     internal class Tag_A : Tag<TagsA> { }

//     internal enum TagsB { B_ENUM_1, B_ENUM_2 }
//     internal class Tag_B : Tag<TagsB> { }

//     public class TagMatchTests
//     {
//         GameObject obj = new GameObject();

//         Tag_A tagA1 = default;
//         Tag_A tagA2 = default;
//         Tag_B tagB1 = default;
//         Tag tagGeneric1 = default;

//         [OneTimeSetUp]
//         public void TestInitialize()
//         {
//             tagA1 = obj.AddComponent<Tag_A>();
//             tagA2 = obj.AddComponent<Tag_A>();
//             tagB1 = obj.AddComponent<Tag_B>();
//         }

//         [Test]
//         public void TrivialTest()
//         {
//             Assert.True(true);
//         }

//         //-------------------------------------------- 3 compahrisons
//         [Test]
//         public void SameTypeSameTag()
//         {
//             tagA1.Type = TagsA.A_ENUM_1;
//             tagA2.Type = TagsA.A_ENUM_1;

//             Assert.IsTrue(tagA1 == tagA2);
//         }

//         [Test]
//         public void SameTypeDifferentTags()
//         {
//             tagA1.Type = TagsA.A_ENUM_1;
//             tagA2.Type = TagsA.A_ENUM_2;
//             Assert.IsTrue(tagA1 != tagA2);
//         }

//         [Test]
//         public void DifferentTypes()
//         {
//             tagA1.Type = TagsA.A_ENUM_1;
//             tagB1.Type = TagsB.B_ENUM_1;

//             Assert.IsTrue(tagA1 != tagB1);
//         }

//         //-------------------------------------------- generic tags
//         [Test]
//         public void GenericTypesPolymorphism()
//         {
//             tagA1.Type = TagsA.A_ENUM_1;
//             tagGeneric1 = tagA1;

//             Assert.True(tagGeneric1 == tagA1);//polymorphysmo
//         }
//     }

// }
