using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Rarity;

public class HealthBoostItem : GenericItem
{
    [Header("Increase max health and give it to player (add value to max current health) (float value)")]
    [SerializeField] private float epicGain;
    [SerializeField] private float legendaryGain;

    protected override void AddToPlayer(Player player)
    {
        // check with loot manager, but isnt suppose to be unique and start at epic
        if (rarity == ItemRarity.EPIC) player.MaxHealthBoost(epicGain);
        else if (rarity == ItemRarity.LEGENDARY) player.MaxHealthBoost(legendaryGain);
    }
}
