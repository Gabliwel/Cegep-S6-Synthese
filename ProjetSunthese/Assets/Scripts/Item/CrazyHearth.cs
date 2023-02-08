using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyHearth : MonoBehaviour
{
    private GameObject player;
    private GameObject weapon;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weapon = GameObject.FindGameObjectWithTag("Weapon");
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().GetDoubleNumber();
            weapon.GetComponent<Weapon>().GetDoubleNumber();
            gameObject.SetActive(false);
        }
    }
}
