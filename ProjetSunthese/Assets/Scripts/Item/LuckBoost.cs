using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckBoost : GenericItem
{
    [Header("Increase luck (float value is a value added to the player current luck)")]
    [SerializeField] private float legendaryGain;

    protected override void AddToPlayer(Player player)
    {
        // check with loot manager, but isnt suppose to be unique and start at legendary
        player.IncreasePlayerLuck(legendaryGain);
    }
}
