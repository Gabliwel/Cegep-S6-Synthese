using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BofrerSceneManager : MonoBehaviour
{
    [SerializeField] Vector3 playerSpawnPosition;
    [SerializeField] AudioClip normal;
    [SerializeField] AudioClip secondPhase;

    private void Start()
    {
        Player.instance.transform.position = playerSpawnPosition;
        MusicMaker.instance.PlayMusic(normal, true);
    }

    public void SwitchToPhase2()
    {
        MusicMaker.instance.FadeTo(secondPhase, true);
    }
}
