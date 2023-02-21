using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageItem : GenericItem
{
    private void Start()
    {
        desc = "Damage item";
    }

    protected override void AddToPlayer(Player player)
    {
        player.BoostDamage();
    }
}
