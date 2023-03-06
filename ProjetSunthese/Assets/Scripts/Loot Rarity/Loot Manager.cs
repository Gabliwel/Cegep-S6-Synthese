using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Rarity;

public class LootManager : MonoBehaviour
{
    [Header("Rarety max rate (order: commun - rare - epic - legendary)")]
    [SerializeField] private float maxCommun = 40;
    [SerializeField] private float maxRare = 70;
    [SerializeField] private float maxEpic = 90;
    [SerializeField] private float maxLegendary = 100;
    [SerializeField] private float maxUnique = 110;

    [Header("Items")]
    [SerializeField] private ItemWithBaseRarityForInspector[] possibleItems;
    [SerializeField] private int numToInstantiate = 4;

    private List<ItemWithRarity> items;

    private void Awake()
    {
        items = new List<ItemWithRarity>();

        for (int i = 0; i < possibleItems.Length; i++)
        {
            for(int j = 0; j < numToInstantiate; j++)
            {
                GameObject obj = Instantiate(possibleItems[i].item);
                obj.SetActive(false);
                items.Add(new ItemWithRarity(obj, possibleItems[i].baseRarity, possibleItems[i].baseRarity));
            }
        }
    }

    // only for test via console for rarity rate
    [ContextMenu("Test rarity")]
    public void TestRarity()
    {
        float luck = Player.instance.Luck;
        
        float prob = Random.Range(luck, maxLegendary);

        ItemRarity rarity = GetRarityByValue(prob);

        ItemWithRarity itemWithRarity = RandItemByRarity(rarity);
    }

    private ItemRarity GetRandRarity(float luck)
    {
        return GetRarityByValue(Random.Range((int)luck, maxLegendary));
    }

    private ItemRarity GetRandRarityWithUnique(float luck)
    {
        return GetRarityByValue(Random.Range((int)luck, maxUnique));
    }

    private ItemRarity GetRarityByValue(float value)
    {
        if (value <= maxCommun) return ItemRarity.COMMUN;
        else if (value <= maxRare) return ItemRarity.RARE;
        else if (value <= maxEpic) return ItemRarity.EPIC;
        else if (value <= maxLegendary) return ItemRarity.LEGENDARY;
        else return ItemRarity.UNIQUE;
    }

    private ItemWithRarity RandItemByRarity(ItemRarity baseRarity)
    {
        List<ItemWithRarity> temp = new List<ItemWithRarity>();
        foreach(ItemWithRarity item in items)
        {
            if(baseRarity != ItemRarity.UNIQUE && item.baseRarity <= baseRarity)
            {
                temp.Add(item);
            }
            else if(baseRarity == ItemRarity.UNIQUE && item.baseRarity == ItemRarity.UNIQUE)
            {
                temp.Add(item);
            }
        }

        if(temp.Count != 0)
        {
            int rand = Random.Range(0, temp.Count);
            temp[rand].currentRarity = baseRarity;
            items.Remove(temp[rand]);
            return temp[rand];
        }

        // if there isnt in list
        List<ItemWithBaseRarityForInspector> temp2 = new List<ItemWithBaseRarityForInspector>();
        foreach (ItemWithBaseRarityForInspector item in possibleItems)
        {
            if (baseRarity != ItemRarity.UNIQUE && item.baseRarity <= baseRarity)
            {
                temp2.Add(item);
            }
            else if (baseRarity == ItemRarity.UNIQUE && item.baseRarity == ItemRarity.UNIQUE)
            {
                temp2.Add(item);
            }
        }
        int rand2 = Random.Range(0, temp2.Count);
        GameObject newItem = Instantiate(temp2[rand2].item);
        newItem.SetActive(false);
        return new ItemWithRarity(newItem, temp2[rand2].baseRarity, baseRarity);
    }

    public ItemWithRarity RequestItem(bool acceptUnique)
    {
        if(!acceptUnique) return RandItemByRarity(GetRandRarity(Player.instance.Luck));
        return RandItemByRarity(GetRandRarityWithUnique(Player.instance.Luck));
    }

    public ItemWithRarity[] RequestItems(int nbItems, bool acceptUnique)
    {
        ItemWithRarity[] items = new ItemWithRarity[nbItems];
        for (int i = 0; i < nbItems; i++)
        {
            items[i] = RequestItem(acceptUnique);
        }
        return items;
    }
}
