using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonStone : GeneralItem
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().AddPoison(1);
            gameObject.SetActive(false);
        }
    }
}
