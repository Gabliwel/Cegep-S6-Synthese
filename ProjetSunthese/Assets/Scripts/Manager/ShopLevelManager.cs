using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLevelManager : MonoBehaviour
{
    private Player player;

    [SerializeField] private Transform startingPosTrans;
    [SerializeField] private string layer;
    [SerializeField] private AudioClip levelMusic;

    private void Awake()
    {
        MusicMaker.instance.PlayMusic(levelMusic, true);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        player.ChangeLayer(layer, layer);
        player.gameObject.transform.position = startingPosTrans.position;
    }
}
