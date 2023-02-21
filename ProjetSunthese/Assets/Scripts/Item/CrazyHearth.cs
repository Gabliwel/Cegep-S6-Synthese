using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyHearth : GenericItem
{
    private void Start()
    {
        desc = "Crazy heart";
    }

    protected override void AddToPlayer(Player player)
    {
        player.GetCrazyHalfHeart();
    }
}
