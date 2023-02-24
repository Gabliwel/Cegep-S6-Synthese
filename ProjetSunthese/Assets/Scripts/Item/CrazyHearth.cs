using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyHearth : GenericItem
{
    protected override void AddToPlayer(Player player)
    {
        player.GetCrazyHalfHeart();
    }
}
