using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Rarity;

public class ChestDropManager : MonoBehaviour
{
    private LootManager lootManager;

    void Awake()
    {
        lootManager = GameObject.FindGameObjectWithTag("LootManager").GetComponent<LootManager>();
    }

    public ItemWithRarity SendRandomItem()
    {
        ItemWithRarity item = lootManager.RequestItem(true);
        return item;
    }
}
