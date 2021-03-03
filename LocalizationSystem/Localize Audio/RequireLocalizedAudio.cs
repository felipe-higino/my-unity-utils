using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace LocalizationSystemAudio
{
    [Serializable]
    public class UnityEvent_SetAudioClip : UnityEvent<AudioClip> { }

    public class RequireLocalizedAudio : MonoBehaviour
    {
        internal static List<RequireLocalizedAudio> AllLocalizableAudio = new List<RequireLocalizedAudio>();

        [SerializeField]
        private SO_LocalizableAudioDatabase database = default;

        [SerializeField]
        private string audioTag = default;

        [SerializeField, Space(15)]
        private UnityEvent_SetAudioClip MethodToSetClip = default;

        [ContextMenu("Update this audio language")]
        public void UpdateThisAudioLanguage()
        {
            var localizedClip =
                database?.GetLocalizedAudioByTag(audioTag);
            if (null == localizedClip)
            {
                Debug.LogError($"audio not found in object {gameObject}");
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

