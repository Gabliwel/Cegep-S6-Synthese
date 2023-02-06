using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    private bool playerInbound = false;
    private GameObject player;
    private GameObject item;
    private int price;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInbound)
        {
            Debug.Log("Bought item");
            /*
            bool bought = player.GetComponent<PlayerStats>().BuyItem(price);

            if(bought) {
                gameObject.SetActive(false);
            }
            */
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
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
