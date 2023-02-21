using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpeed : GenericItem
{
    private void Start()
    {
        desc = "Movement speed";
    }

    protected override void AddToPlayer(Player player)
    {
        player.BoostPlayerSpeed();
    }
}
