using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonStone : GenericItem
{
    protected override void AddToPlayer(Player player)
    {
        player.AddPoison(1);
    }
}
