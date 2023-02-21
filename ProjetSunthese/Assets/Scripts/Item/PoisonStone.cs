using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonStone : GenericItem
{
    private void Start()
    {
        desc = "Poison speed";
    }

    protected override void AddToPlayer(Player player)
    {
        player.AddPoison(1);
    }
}
