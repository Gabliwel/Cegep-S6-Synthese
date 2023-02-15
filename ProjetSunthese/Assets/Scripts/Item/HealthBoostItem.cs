using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoostItem : GeneralItem
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().MaxHealthBoost(10f);
            gameObject.SetActive(false);
        }
    }
}
