using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : GeneralItem
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().IncreaseAttackSpeed(1);
            gameObject.SetActive(false);
        }
    }
}
