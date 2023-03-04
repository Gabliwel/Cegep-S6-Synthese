using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Rarity;

public class DamageItem : GenericItem
{
    [Header("Increase attack (add value to multiplicator) (float value)")]
    [SerializeField] private float rareGain;
    [SerializeField] private float epicGain;
    [SerializeField] private float legendaryGain;

    protected override void AddToPlayer(Player player)
    {
        // check with loot manager, but isnt suppose to be unique and start at rare
        if (rarity == ItemRarity.RARE) player.BoostDamage(rareGain);
        else if (rarity == ItemRarity.EPIC) player.BoostDamage(epicGain);
        else if (rarity == ItemRarity.LEGENDARY) player.BoostDamage(legendaryGain);
    }
}
