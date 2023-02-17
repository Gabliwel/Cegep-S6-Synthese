using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLevelManager : MonoBehaviour
{
    private Player player;

    [SerializeField] private Transform startingPosTrans;
    [SerializeField] private string layer;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        player.ChangeLayer(layer, layer);
        player.gameObject.transform.position = startingPosTrans.position;
    }
}
