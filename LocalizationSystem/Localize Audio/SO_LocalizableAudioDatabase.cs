using System;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

namespace LocalizationSystemAudio
{
    [Serializable]
    internal class ClipsWithSameTag
    {
        public string Tag = default;
        public List<AudioClip> Clips = default;
    }

    [CreateAssetMenu(fileName = "SO_LocalizableAudioDatabase", menuName = "Felipe Utils/Localizable Audio Database", order = 50)]
    public class SO_LocalizableAudioDatabase : ScriptableObject
    {
        [SerializeField]
        private List<ClipsWithSameTag> audioClipsTable = default;

        static internal async Task Init()
        {
            await Task.CompletedTask;
        }

        public AudioClip GetLocalizedAudioByTag(string tag)
        {
            var tagInManyLanguages = audioClipsTable?.Where(x => x.Tag == tag)?.FirstOrDefault();
            var languageIndex = LocalizationSystem.LanguageIndex;
            var audio = tagInManyLanguages?.Clips?.ElementAtOrDefault(languageIndex);
            return audio;
        }
    }
}
