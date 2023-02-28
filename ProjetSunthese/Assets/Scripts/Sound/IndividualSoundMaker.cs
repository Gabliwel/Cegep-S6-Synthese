using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualSoundMaker : MonoBehaviour
{
    private AudioSource src;
    private bool gotSrc = false;
    private bool infiniteGoing = false;

    private void OnEnable()
    {
        infiniteGoing = false;
    }

    public void PlayAtPoint(AudioClip clip, Vector2 position)
    {
        if (!gotSrc) src = gameObject.GetComponent<AudioSource>();
        gameObject.transform.position = position;
        StartCoroutine(WaitEndOfClip(clip));
    }

    public void PlayAtPoint(AudioClip clip, Vector2 position, float volume)
    {
        if (!gotSrc) src = gameObject.GetComponent<AudioSource>();
        gameObject.transform.position = position;
        StartCoroutine(WaitEndOfClipVolume(clip, volume));
    }

    public void InfinitePlayAtPoint(AudioClip clip, Vector2 position, float volume)
    {
        if (!gotSrc) src = gameObject.GetComponent<AudioSource>();
        gameObject.transform.position = position;

        if (!infiniteGoing)
        {
            StartCoroutine(ContinuePlay(clip, volume));
        }
    }

    private IEnumerator WaitEndOfClip(AudioClip clip)
    {
        src.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        gameObject.SetActive(false);
    }

    private IEnumerator WaitEndOfClipVolume(AudioClip clip, float volume)
    {
        src.PlayOneShot(clip);
        src.volume = volume;
        yield return new WaitForSeconds(clip.length);
        gameObject.SetActive(false);
    }

    private IEnumerator ContinuePlay(AudioClip clip, float volume)
    {
        infiniteGoing = true;
        src.volume = volume;
        while (true)
        {
            src.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length / 2);
        }
    }
}
