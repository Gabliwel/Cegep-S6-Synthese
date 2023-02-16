using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Rarity;

namespace Billy.Rarity
{
    // For beta usage
    public enum ItemRarity
    {
        COMMUN,
        RARE,
        EPIC,
        LEGENDARY,
        UNIQUE // unique wont change value
    }
}

public abstract class GenericItem : Interactable
{
    private ItemRarity rarity;

    void Start()
    {
        rarity = ItemRarity.COMMUN;
    }

    public override void Interact(Player player) 
    {
        AddToPlayer(player);
        Collected(player);
    }

    protected abstract void AddToPlayer(Player player);

    private void Collected(Player player)
    {

    }
}
