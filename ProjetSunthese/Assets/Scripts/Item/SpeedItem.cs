using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : MonoBehaviour
{
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerMovement>().SpeedItemPickup();
            gameObject.SetActive(false);
        }
    }
}
