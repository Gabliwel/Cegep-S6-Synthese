using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoostItem : GenericItem
{
    private void Start()
    {
        desc = "Health boost";
    }

    protected override void AddToPlayer(Player player)
    {
        player.MaxHealthBoost(10f);
    }
}
