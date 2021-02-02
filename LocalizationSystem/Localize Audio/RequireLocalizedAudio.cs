using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RequireLocalizedAudio : MonoBehaviour
{
    internal static List<RequireLocalizedAudio> AllLocalizableAudio = new List<RequireLocalizedAudio>();

    [SerializeField]
    private string audioTag = default;

    [SerializeField, Space(15)]
    private AudioSource source = default;

    [ContextMenu("Update this audio language")]
    public void UpdateThisAudioLanguage()
    {
        var localizedClip =
            LocalizableAudioSheet.GetLocalizedAudioByTag(audioTag);
        if (null == localizedClip)
        {
            Debug.LogError("invalid audio clip");
            return;
        }
        source.clip = localizedClip;
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
