using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    public void PlayAudio(AudioClip audio, float volume = 1f, bool loop = false)
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                audioSources[i].clip = audio;
                if (volume != 1f)
                    audioSources[i].volume = volume;

                audioSources[i].Play();
                break;
            }

            if (i == audioSources.Length - 1)
            {
                AudioSource newAudioSource = transform.gameObject.AddComponent<AudioSource>();
                newAudioSource.loop = loop;
                newAudioSource.playOnAwake = false;
                newAudioSource.clip = audio;

                if (volume != 1f)
                    newAudioSource.volume = volume;
                newAudioSource.Play();
                break;
            }
        }
    }
}
