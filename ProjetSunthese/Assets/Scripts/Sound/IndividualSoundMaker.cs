using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualSoundMaker : MonoBehaviour
{
    private AudioSource src;
    private bool gotSrc = false;

    public void PlayAtPoint(AudioClip clip, Vector2 position)
    {
        if (!gotSrc) src = gameObject.GetComponent<AudioSource>();
        gameObject.transform.position = position;
        StartCoroutine(WaitEndOfClip(clip));
    }

    private IEnumerator WaitEndOfClip(AudioClip clip)
    {
        src.PlayOneShot(clip);
        yield return new WaitForSeconds(clip.length);
        gameObject.SetActive(false);
    }
}
