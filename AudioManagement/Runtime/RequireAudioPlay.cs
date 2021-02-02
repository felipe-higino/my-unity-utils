using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class RequireAudioPlay : MonoBehaviour
{
    [SerializeField]
    private AudioMixerGroup group = null;

    [SerializeField]
    private AudioClip clip = null;

    public AudioClip Clip
    {
        get => clip;
        set => clip = value;
    }

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

    [ContextMenu("play clip")]
    public void PlayAudioClip()
    {
        AudioManager?.PlayOneShoot(clip, ClipSource);
    }

    [ContextMenu("play clip overriding")]
    public void PlayClipOverriding()
    {
        AudioManager?.PlayLonely(clip, ClipSource);
    }

    [ContextMenu("Stop clip")]
    public void StopAudioClip()
    {
        AudioManager?.StopSource(ClipSource);
    }

    [ContextMenu("Pause clip")]
    public void PauseAudioClip()
    {
        AudioManager?.PauseSource(ClipSource);
    }

    [ContextMenu("Unpause clip")]
    public void UnpauseAudioClip()
    {
        AudioManager?.UnpauseSource(ClipSource);
    }
}
