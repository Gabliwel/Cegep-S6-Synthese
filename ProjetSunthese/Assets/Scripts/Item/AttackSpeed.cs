using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : GenericItem
{
    protected override void AddToPlayer(Player player)
    {
        player.IncreaseAttackSpeed(1);
    }
}
