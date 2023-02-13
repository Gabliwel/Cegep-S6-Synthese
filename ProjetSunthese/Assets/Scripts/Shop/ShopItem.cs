using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : Interactable
{
    private GameObject item;
    private SpriteRenderer itemSprite;
    [SerializeField] private int price;

    public void SetItem(GameObject newItem)
    {
        item = newItem;
        itemSprite = item.GetComponent<SpriteRenderer>();
    }

    public override void ChangeSelectedState(bool selected)
    {
        if (selected)
        {
            //sprite.material = selectedMaterial;
            itemSprite.material = selectedMaterial;
        }
        else
        {
            //sprite.material = defaultMaterial;
            itemSprite.material = defaultMaterial;
        }
    }

    public override void Interact(Player player) 
    {
        bool bought = player.BuyItem(price);

        if (bought)
        {
            item.transform.parent = null;
            item.GetComponent<ShopItemMovement>().IsNowSold();
            gameObject.SetActive(false);
        }
    }
}
