using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RequireAudioPlay : MonoBehaviour
{
    [SerializeField] AudioMixerGroup group = null;
    [SerializeField] AudioClip clip = null;

    Unique_AudioManager AudioManager => Unique<Unique_AudioManager>.Get();

    AudioSource _clipSource = null;
    public AudioSource ClipSource
    {
        get
        {
            if (_clipSource == null)
                _clipSource = AudioManager?.GetSourceFromGroup(group);
            return _clipSource;
        }
    }

    public float AudioLength => clip.length;

    public bool IsPlaying
    {
        get
        {
            if (ClipSource == null)
                return false;

            return ClipSource.isPlaying;
        }
    }

    public void PlayAudioClip()
    {
        AudioManager?.PlayOneShoot(clip, ClipSource);
    }

    public void PlayClipOverriding()
    {
        AudioManager?.PlayLonely(clip, ClipSource);
    }

    public void StopAudioClip()
    {
        AudioManager?.StopSource(ClipSource);
    }

    public void PauseAudioClip()
    {
        AudioManager?.PauseSource(ClipSource);
    }

    public void UnpauseAudioClip()
    {
        AudioManager?.UnpauseSource(ClipSource);
    }
}
