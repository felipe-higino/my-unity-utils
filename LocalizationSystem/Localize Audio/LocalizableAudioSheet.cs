// using System.Linq;
// using System.Collections;
// using System.Threading.Tasks;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AddressableAssets;

// namespace LocalizationSystemAudio
// {

//     internal static class LocalizableAudioSheet
//     {
//         private static List<string> Tags = new List<string>();
//         private static SO_LocalizableAudioDatabase AudioDatabase = default;

//         static internal async Task Init()
//         {
//             AudioDatabase =
//                 await Addressables.LoadAssetAsync<SO_LocalizableAudioDatabase>("AudioTags").Task;
//             if (null == AudioDatabase)
//             {
//                 Debug.LogError("Addressable with tag \"AudioTags\" not found");
//                 return;
//             }

//             foreach (var tagableClip in AudioDatabase.ClipsTable)
//             {
//                 Tags.Add(tagableClip.Tag);
//             }
//         }

//         internal static AudioClip GetLocalizedAudioByTag(string tag)
//         {
//             if (null == AudioDatabase)
//             {
//                 Debug.LogError("Audio database is null");
//                 return null;
//             }

//             var languageIndex = LocalizationSystem.LanguageIndex;
//             var tagIndex = Tags.IndexOf(tag);

//             return AudioDatabase.ClipsTable
//                 .ElementAtOrDefault(tagIndex)?.Clips
//                 .ElementAtOrDefault(languageIndex);
//         }
//     }

// }
