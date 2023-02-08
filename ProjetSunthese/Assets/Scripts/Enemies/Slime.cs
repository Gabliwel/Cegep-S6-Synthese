using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Slime : Enemy
{
    [SerializeField] float damageDealt;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        hp = 100;
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
