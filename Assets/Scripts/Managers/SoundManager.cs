using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    SFX_BALL_HIT,
    SFX_CONFETTI,
    SFX_ELECTRIC_SHOCK,
    SFX_LAUGHTER_1,
    SFX_LAUGHTER_2,
    SFX_LAUGHTER_3,
    SFX_DRINKING,
}


public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private AudioSource _audioSource;

    [Header("Ball Sounds")]
    [SerializeField]
    private List<Sound> _soundList;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        _audioSource = GetComponent<AudioSource>();
    }


    public void PlayOneShot(SoundType soundType, AudioSource audioSource)
    {
        foreach(Sound sound in _soundList)
        {
            if(sound.soundType == soundType)
                audioSource.PlayOneShot(sound.clip);
        }
    }

    public void PlayOneShot(SoundType soundType)
    {
        foreach(Sound sound in _soundList)
        {
            if(sound.soundType == soundType)
                _audioSource.PlayOneShot(sound.clip);
        }
    }


    public void PlayLoop(SoundType soundType)
    {
        _audioSource.loop = true;
        foreach (Sound sound in _soundList)
        {
            if (sound.soundType == soundType)
                _audioSource.clip = sound.clip;
        }
        _audioSource.Play();
    }

    public void PlayLoop(SoundType soundType, AudioSource audioSource)
    {
        audioSource.loop = true;
        foreach (Sound sound in _soundList)
        {
            if (sound.soundType == soundType)
                audioSource.clip = sound.clip;
        }
        audioSource.Play();
    }

    public void StopAudioSource()
    {
        _audioSource.Stop();
    }

    public void StopAudioSource(AudioSource audioSource)
    {
        audioSource.Stop();
    }

}

[Serializable]
public struct Sound
{
    public SoundType soundType;
    public AudioClip clip;

}
