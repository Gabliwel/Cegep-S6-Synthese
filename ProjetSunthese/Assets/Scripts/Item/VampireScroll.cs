using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireScroll : GenericItem
{
    [SerializeField] private float suckAmmount;
    protected override void AddToPlayer(Player player)
    {
        player.GainBloodSuck(suckAmmount);
    }
}
