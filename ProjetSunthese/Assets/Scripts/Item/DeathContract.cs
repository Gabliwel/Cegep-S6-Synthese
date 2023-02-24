using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathContract : GenericItem
{
    protected override void AddToPlayer(Player player)
    {
        // only legendary
        player.GainDeathContract();
    }
}
