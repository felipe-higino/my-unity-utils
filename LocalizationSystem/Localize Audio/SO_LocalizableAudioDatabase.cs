using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_AudioTags", menuName = "Localizable Audio Database", order = 0)]
internal class SO_LocalizableAudioDatabase : ScriptableObject
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