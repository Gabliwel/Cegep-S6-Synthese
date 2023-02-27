using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Rarity;

public class HealingPlayer : GenericItem
{
    [Header("Heal percent of health (float value is a decimal percentage)")]
    [SerializeField] private float rareGain;
    [SerializeField] private float epicGain;
    [SerializeField] private float legendaryGain;

    protected override void AddToPlayer(Player player)
    {
        // check with loot manager, but isnt suppose to be unique and start at rare
        if (rarity == ItemRarity.RARE) player.HealPercent(rareGain);
        else if (rarity == ItemRarity.EPIC) player.HealPercent(epicGain);
        else if (rarity == ItemRarity.LEGENDARY) player.HealPercent(legendaryGain);
    }   
}
