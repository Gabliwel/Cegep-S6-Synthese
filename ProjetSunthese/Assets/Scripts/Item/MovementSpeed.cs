using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeed : GenericItem
{
    protected override void AddToPlayer(Player player)
    {
        player.BoostPlayerSpeed();
    }
}
