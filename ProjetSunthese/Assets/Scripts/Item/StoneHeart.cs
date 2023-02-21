using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHeart : GenericItem
{
    private void Start()
    {
        desc = "Stone heart";
    }

    protected override void AddToPlayer(Player player)
    {
        player.GainStoneHeart();
    }
}
