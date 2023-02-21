using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : GenericItem
{
    private void Start()
    {
        desc = "Attack speed";
    }

    protected override void AddToPlayer(Player player)
    {
        player.IncreaseAttackSpeed(1);
    }
}
