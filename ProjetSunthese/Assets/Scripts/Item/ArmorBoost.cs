using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorBoost : GenericItem
{
    protected override void AddToPlayer(Player player)
    {
        player.GainArmor(1f);
    }
}
