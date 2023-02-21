using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPlayer : GenericItem
{
    private void Start()
    {
        desc = "Healing player";
    }

    protected override void AddToPlayer(Player player)
    {
        player.HealPercent(0.10f);
    }   
}
