using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : WeaponsChange
{
    void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInbound)
        {
            weapon.GetComponent<Weapon>().SwitchWeapon(1);
        }
    }
}
