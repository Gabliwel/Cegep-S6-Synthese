using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBoost : GenericItem
{
    private void Start()
    {
        desc = "Armor boost";
    }

    protected override void AddToPlayer(Player player)
    {
        player.GainArmor(1f);
    }
}
