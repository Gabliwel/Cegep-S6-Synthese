using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireScroll : GenericItem
{
    protected override void AddToPlayer(Player player)
    {
        player.GainBloodSuck();
    }
}
