using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : WeaponsChange
{
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInbound)
        {
            player.GetComponent<Player>().SwitchWeaponType(3);
        }
    }
}
