using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHeart : GenericItem
{
    protected override void AddToPlayer(Player player)
    {
        player.GainStoneHeart();
    }
}
