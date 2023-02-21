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
        LEGENDARY//,
        //UNIQUE // unique wont change value
    }

    [System.Serializable]
    public class ItemWithBaseRarity
    {

        public GameObject item;
        public ItemRarity baseRarity;

        public ItemWithBaseRarity(GameObject item, ItemRarity baseRarity)
        {
            this.item = item;
            this.baseRarity = baseRarity;
        }
    }
}
