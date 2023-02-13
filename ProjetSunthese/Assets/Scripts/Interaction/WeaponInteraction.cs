using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteraction : Interactable
{
    public override void Interact(Player player)
    {
        player.SwitchWeapon(this.gameObject);
    }
}
