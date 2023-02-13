using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShop : MonoBehaviour
{
    private bool playerInbound = false;
    private GameObject player;
    [SerializeField] private GameObject item;
    private Player playerInfo;
    [SerializeField] private int price;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInfo = player.GetComponent<Player>();
        item = Instantiate(item);
        item.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInbound)
        {
            Debug.Log("Trying to buy");
            bool bought = playerInfo.BuyItem(price);

            if(bought) {
                gameObject.SetActive(false);
                item.transform.position = gameObject.transform.position;
                item.SetActive(true);
            }
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
