using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoSceneManager : MonoBehaviour
{
    [SerializeField] private Vector3 staringPosition;
    [SerializeField] private AudioClip levelMusic;

    void Start()
    {
        MusicMaker.instance.PlayMusic(levelMusic, true);
        Player player = Player.instance;
        player.ChangeLayer("Layer 1", "Layer 1");
        player.gameObject.transform.position = staringPosition;

    }
}
