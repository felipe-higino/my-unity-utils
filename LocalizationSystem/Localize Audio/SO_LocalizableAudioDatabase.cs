using System;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace LocalizationSystemAudio
{
    [Serializable]
    internal class ClipsWithSameTag
    {
        public string Tag = default;
        public List<AssetReferenceAudioClip> Clips = default;
    }

    [Serializable]
    public class AssetReferenceAudioClip : AssetReferenceT<AudioClip>
    {
        public AssetReferenceAudioClip(string guid) : base(guid)
        {
        }
    }

    [CreateAssetMenu(fileName = "SO_LocalizableAudioDatabase", menuName = "Felipe Utils/Localizable Audio Database", order = 50)]
    public class SO_LocalizableAudioDatabase : ScriptableObject
    {
        [SerializeField]
        private List<ClipsWithSameTag> audioClipsTable = default;

        private List<string> tags = null;
        private List<List<AudioClip>> listOfClips = new List<List<AudioClip>>();

        static internal async Task Init()
        {
            var taskPool = new List<Task>();

            var audioDatabases =
                await Addressables.LoadAssetsAsync<SO_LocalizableAudioDatabase>("Localization", null).Task;

            if (null == audioDatabases)
                return;

            //initializing tags for each database
            foreach (var database in audioDatabases)
            {
                database.tags = new List<string>();
                foreach (var line in database.audioClipsTable)
                {
                    database.tags.Add(line.Tag);
                    var loadTask = database.LoadAllAudioClips();
                    taskPool.Add(loadTask);
                }
            }
            await Task.WhenAll(taskPool);
        }

        private async Task LoadAllAudioClips()
        {
            listOfClips.Clear();
            foreach (var clipsWithSameTag in audioClipsTable)
            {
                var listOfClipsWithSameTag = new List<AudioClip>();
                foreach (var clipAssetReference in clipsWithSameTag.Clips)
                {
                    var clip = await clipAssetReference.LoadAssetAsync().Task;
                    listOfClipsWithSameTag.Add(clip);
                }
                listOfClips.Add(listOfClipsWithSameTag);
            }
        }

        public AudioClip GetLocalizedAudioByTag(string tag)
        {
            if (null == tags)
            {
                Debug.LogError("Error getting tags, database not found in Addressables");
                return null;
            }

            var tagIndex = tags.IndexOf(tag);
            var languageIndex = LocalizationSystem.LanguageIndex;

            return listOfClips?
                .ElementAtOrDefault(tagIndex)?
                .ElementAtOrDefault(languageIndex);
        }
    }
}
