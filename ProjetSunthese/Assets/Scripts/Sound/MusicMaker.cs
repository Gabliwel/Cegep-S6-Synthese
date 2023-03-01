using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMaker : MonoBehaviour
{
    public static MusicMaker instance = null;
    [SerializeField] private float fadeDuration;
    private const float DEFAULT_VOLUME = 0.3f;
    private AudioSource audioSource;
    private AudioClip currentClip;
    private bool fading = false;
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

    public void FadeTo(AudioClip clip, bool loop)
    {
        if (fading) return;
        fading = true;
        audioSource.loop = loop;
        StartCoroutine(FadeMusic(clip));
    }

    private IEnumerator FadeMusic(AudioClip clip)
    {
        float counter = 0;
        float tick = audioSource.volume / fadeDuration;

        while (counter < fadeDuration)
        {
            counter += Time.deltaTime;
            audioSource.volume -= tick * Time.deltaTime;
            yield return null;
        }
        currentClip = clip;
        PlayClip();

        audioSource.volume = DEFAULT_VOLUME;
        fading = false;
    }


    private void PlayClip()
    {
        audioSource.Stop();
        audioSource.clip = currentClip;
        audioSource.Play();
    }
}
