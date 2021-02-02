using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace LocalizationSystemAudio
{
    [Serializable]
    public class UnityEvent_SetAudioClip : UnityEvent<AudioClip> { }

    [Serializable]
    public class AssetReferenceAudioDatabase : AssetReferenceT<SO_LocalizableAudioDatabase>
    {
        public AssetReferenceAudioDatabase(string guid) : base(guid)
        {
        }
    }

    public class RequireLocalizedAudio : MonoBehaviour
    {
        internal static List<RequireLocalizedAudio> AllLocalizableAudio = new List<RequireLocalizedAudio>();

        [SerializeField]
        private AssetReferenceAudioDatabase database = default;

        [SerializeField]
        private string audioTag = default;

        [SerializeField, Space(15)]
        private UnityEvent_SetAudioClip MethodToSetClip = default;

        [ContextMenu("Update this audio language")]
        public async void UpdateThisAudioLanguage()
        {
            var db = await database.LoadAssetAsync().Task;

            var localizedClip =
                db?.GetLocalizedAudioByTag(audioTag);
            if (null == localizedClip)
            {
                Debug.LogError("invalid audio clip");
                return;
            }
            MethodToSetClip?.Invoke(localizedClip);
        }

        private void Awake()
        {
            AllLocalizableAudio.Add(this);
        }

        private void OnDestroy()
        {
            AllLocalizableAudio.Remove(this);
        }
    }
}

