using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LocalizationSystemAudio
{
    [CreateAssetMenu(fileName = "SO_LocalizableAudioDatabase", menuName = "Felipe Utils/Localizable Audio Database", order = 50)]
    public class SO_LocalizableAudioDatabase : ScriptableObject
    {
        [SerializeField]
        private List<TagableClip> clipsTable = default;
        internal List<TagableClip> ClipsTable => clipsTable;

        [Serializable]
        internal class TagableClip
        {
            public string Tag = default;
            public List<AudioClip> Clips = default;
        }
    }
}
