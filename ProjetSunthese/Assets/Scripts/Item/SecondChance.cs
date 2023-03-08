using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondChance : GenericItem
{
    [SerializeField] private float speedDecrease;
    protected override void AddToPlayer(Player player)
    {
        //Only unique
        player.GainSecondChance(speedDecrease);
    }
}
