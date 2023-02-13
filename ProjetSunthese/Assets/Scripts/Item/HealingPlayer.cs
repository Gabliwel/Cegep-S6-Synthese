using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPlayer : GeneralItem
{
    [SerializeField] int healAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().Heal(healAmount);
            gameObject.SetActive(false);
        }
    }
}
