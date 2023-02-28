using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMaker : MonoBehaviour
{
    public static MusicMaker instance = null;
    private const float DEFAULT_VOLUME = 0.3f;
    private AudioSource audioSource;
    private AudioClip currentClip;
    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = DEFAULT_VOLUME;
    }

    public void PlayMusic(AudioClip clip, bool loop)
    {
        currentClip = clip;
        audioSource.loop = loop;
        PlayClip();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }


    private void PlayClip()
    {
        audioSource.Stop();
        audioSource.clip = currentClip;
        audioSource.Play();
    }
}
