using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : WeaponsChange
{
    void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInbound)
        {
            weapon.GetComponent<Weapon>().SwitchWeapon(2);
        }
    }
}
