using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : Interactable
{
    private GameObject item;
    private SpriteRenderer itemSprite;
    private TMP_Text text;

    private int price;

    public void SetItem(GameObject newItem, int newPrice)
    {
        price = newPrice;
        item = newItem;

        itemSprite = item.GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TMP_Text>();
        itemSprite.sortingOrder = 5;
        text.text = newPrice.ToString();
    }

    public override void ChangeSelectedState(bool selected, DescriptionBox descBox)
    {
        if (selected)
        {
            itemSprite.material = selectedMaterial;
        }
        else
        {
            itemSprite.material = defaultMaterial;
        }
    }

    public override void Interact(Player player) 
    {
        bool bought = player.BuyItem(price);

        if (bought)
        {
            item.transform.parent = null;
            item.GetComponent<ShopItemMovement>().IsNowSold(itemSprite);
            gameObject.SetActive(false);
            player.UpdateInteractables(this);
        }
    }
}
