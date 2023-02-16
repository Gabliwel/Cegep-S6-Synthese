using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageItem : GenericItem
{
    protected override void AddToPlayer(Player player)
    {
        player.BoostDamage();
    }
}
