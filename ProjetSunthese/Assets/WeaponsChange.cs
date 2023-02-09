using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponsChange : MonoBehaviour
{
    protected GameObject player;
    protected bool playerInbound = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInbound = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInbound = false;
        }
    }
}
