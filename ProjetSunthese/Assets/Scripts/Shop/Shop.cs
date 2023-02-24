using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Rarity;

public class Shop : MonoBehaviour
{
    [Header("Shop with 3 stands")]
    [SerializeField] private GameObject[] stands;
    [SerializeField] private List<GameObject> possibleOtherItem;

    private LootManager lootManager;
    private const string shopLayer = "Layer 1";

    void Awake()
    {
        lootManager = GameObject.FindGameObjectWithTag("LootManager").GetComponent<LootManager>();
    }

    private void Start()
    {
        ItemWithRarity item1 = lootManager.RequestItem(false);
        item1.item.transform.position = stands[0].transform.position + (Vector3.up * 2);
        item1.item.transform.parent = stands[0].transform;
        item1.item.GetComponent<GenericItem>().SetItem(shopLayer, item1.currentRarity);
        PrepareItem(item1.item, 0, item1.currentRarity);

        ItemWithRarity item2 = lootManager.RequestItem(true);
        item2.item.transform.position = stands[1].transform.position + (Vector3.up * 2);
        item2.item.transform.parent = stands[1].transform;
        item2.item.GetComponent<GenericItem>().SetItem(shopLayer, item2.currentRarity);
        PrepareItem(item2.item, 1, item2.currentRarity);

        int nb = Random.Range(0, possibleOtherItem.Count);
        GameObject item3 = Instantiate(possibleOtherItem[nb], stands[2].transform);
        PrepareItem(item3, 2);
    }

    private void PrepareItem(GameObject item, int standIndex)
    {
        int price = BaseItem(item, standIndex);
        stands[standIndex].GetComponent<ShopItem>().SetItem(item, price);
        item.SetActive(true);
    }

    private void PrepareItem(GameObject item, int standIndex, ItemRarity rarity)
    {
        int price = BaseItem(item, standIndex);
        stands[standIndex].GetComponent<ShopItem>().SetItem(item, price, rarity);
        item.SetActive(true);
    }

    private int BaseItem(GameObject item, int standIndex)
    {
        Interactable itemInt = item.GetComponent<Interactable>();
        int basePrice = itemInt.GetPrice();
        itemInt.enabled = false;
        item.transform.GetChild(0).gameObject.SetActive(false);
        item.AddComponent<ShopItemMovement>();
        return basePrice;
    }
}
