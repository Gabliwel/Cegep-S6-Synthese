using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondChance : GenericItem
{
    private float speedDecrease = -1f;
    protected override void AddToPlayer(Player player)
    {
        //Only unique
        player.GainSecondChance(speedDecrease);
    }
}
