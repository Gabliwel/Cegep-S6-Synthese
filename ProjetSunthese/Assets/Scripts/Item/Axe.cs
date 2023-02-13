using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : WeaponsChange
{
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

   /* void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInbound)
        {
            player.GetComponent<Player>().SwitchWeaponType(2);
            gameObject.SetActive(false);
        }
    }*/
}
