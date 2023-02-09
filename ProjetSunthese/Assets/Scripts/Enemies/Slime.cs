using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Slime : Enemy
{
    private GameObject player;
    private void Start()
    {
        scaling = GameObject.FindGameObjectWithTag("Scaling").GetComponent<Scaling>();
        player = GameObject.FindGameObjectWithTag("Player");
        scalingLevel = scaling.SendScaling();
        hp = 30 * scalingLevel;
    }

    protected override void Drop()
    {
        player.GetComponent<Player>().HealBloodSuck(2);
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
