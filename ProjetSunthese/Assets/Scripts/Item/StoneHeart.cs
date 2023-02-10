using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHeart : GeneralItem
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().GainStoneHeart();
            gameObject.SetActive(false);
        }
    }
}
