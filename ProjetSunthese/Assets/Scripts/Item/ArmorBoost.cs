using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Rarity;

public class ArmorBoost : GenericItem
{
    [Header("Ignore damage (float value)")]
    [SerializeField] private float communGain;
    [SerializeField] private float rareGain;
    [SerializeField] private float epicGain;
    [SerializeField] private float legendaryGain;

    protected override void AddToPlayer(Player player)
    {
        // check with loot manager, but isnt suppose to be unique and start at commun
        if (rarity == ItemRarity.COMMUN) player.GainArmor(communGain);
        else if (rarity == ItemRarity.RARE) player.GainArmor(rareGain);
        else if (rarity == ItemRarity.EPIC) player.GainArmor(epicGain);
        else if (rarity == ItemRarity.LEGENDARY) player.GainArmor(legendaryGain);
    }
}
