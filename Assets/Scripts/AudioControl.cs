using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundSource;

    public static Action OnPlayClickSound;
    

    private void Awake()
    {
        OnPlayClickSound += PlayClickSound;
        PlayMusic();
    }

    private void PlayClickSound()
    {
        _soundSource.Play();
    }

    private void PlayMusic()
    {
        _musicSource.Play();
    }
}
