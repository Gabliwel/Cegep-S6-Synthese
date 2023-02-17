using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPlayer : GenericItem
{
    protected override void AddToPlayer(Player player)
    {
        player.HealPercent(0.10f);
    }   
}
