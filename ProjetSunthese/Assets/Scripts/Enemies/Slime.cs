using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Slime : Enemy
{
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        goldDropped = 5;
        xpGiven = 5;
    }

    protected override void Drop()
    {
        player.GetComponent<Player>().GainDrops(2, xpGiven, goldDropped);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().Harm(damageDealt);
            gameObject.SetActive(false);
        }
    }
}
