using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Unique_AudioManager : MonoBehaviour
{
    [Header("Caching")]
    [SerializeField] AudioMixer mixer = null;
    List<AudioSource> sources = new List<AudioSource>();

    private void Awake()
    {
        Unique<Unique_AudioManager>.Generate(this);

        foreach (var group in mixer.FindMatchingGroups("Master"))
        {
            var src = gameObject.AddComponent<AudioSource>();
            src.outputAudioMixerGroup = group;
            src.loop = false;
            sources.Add(src);
        }
    }

    public void PlayOneShoot(AudioClip clip, AudioSource source)
    {
        source?.PlayOneShot(clip);
    }

    public void PlayLonely(AudioClip clip, AudioSource source)
    {
        if (source == null)
            return;
        source.Stop();
        source.clip = clip;
        source.Play();
    }

    public void StopSource(AudioSource source)
    {
        source?.Stop();
    }

    public void PauseSource(AudioSource source)
    {
        source?.Pause();
    }

    public void UnpauseSource(AudioSource source)
    {
        source?.UnPause();
    }

    public AudioSource GetSourceFromGroup(AudioMixerGroup group)
    {
        foreach (var source in sources)
        {
            //searching for the mixer in sources list
            var _group = source.outputAudioMixerGroup;
            if (_group == group)
                return source;
        }
        return null;
    }

    public void StopAllAudioSources()
    {
        foreach (var source in sources)
        {
            StopSource(source);
        }
    }
}
