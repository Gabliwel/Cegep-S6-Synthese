using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> voiceClips;
    [SerializeField] private float frequency;
    private float timer;
    private bool playing;

    public void PlayRandomVoiceClips()
    {
        playing = true;
    }

    public void StopRandomVoiceClips()
    {
        playing = false;
        timer = 0;
    }

    private void Update()
    {
        if(playing)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else
            {
                timer = frequency;
                SoundMaker.instance.RequestSound(Camera.main.transform.position, GetRandomVoiceClip());
            }
        }
    }

    private AudioClip GetRandomVoiceClip()
    {
        return voiceClips[Random.Range(0, voiceClips.Count)];
    }
}
