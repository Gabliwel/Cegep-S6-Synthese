using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Rarity;

namespace Billy.Rarity
{
    public enum ItemRarity
    {
        COMMUN,
        RARE,
        EPIC,
        LEGENDARY,
        UNIQUE // unique wont change value (no scale)
    }

    [System.Serializable]
    public class ItemWithBaseRarityForInspector
    {

        public GameObject item;
        public ItemRarity baseRarity;

        public ItemWithBaseRarityForInspector(GameObject item, ItemRarity baseRarity)
        {
            this.item = item;
            this.baseRarity = baseRarity;
        }
    }
    
    public class ItemWithRarity
    {

        public GameObject item;
        public ItemRarity baseRarity;
        public ItemRarity currentRarity;

        public ItemWithRarity(GameObject item, ItemRarity baseRarity, ItemRarity currentRarity)
        {
            this.item = item;
            this.baseRarity = baseRarity;
            this.currentRarity = currentRarity;
        }
    }
}
