using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopItem : Interactable
{
    private GameObject item;
    private SpriteRenderer itemSprite;
    private TMP_Text text;
    private SoundMaker soundMaker;

    private int price;

    private void Start()
    {
        soundMaker = GameObject.FindGameObjectWithTag("SoundMaker").GetComponent<SoundMaker>();
    }

    public void SetItem(GameObject newItem, int newPrice)
    {
        price = newPrice;
        item = newItem;

        desc = newItem.GetComponent<Interactable>().Desc;

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
            if (UseTextBox) descBox.PopUp(desc);
        }
        else
        {
            itemSprite.material = defaultMaterial;
            if (UseTextBox) descBox.Close();
        }
    }

    public override void Interact(Player player) 
    {
        bool bought = player.BuyItem(price);

        if (bought)
        {
            soundMaker.BuyItemSound(transform.position);
            item.transform.parent = null;
            item.GetComponent<ShopItemMovement>().IsNowSold(itemSprite);
            gameObject.SetActive(false);
            player.UpdateInteractables(this);
        }
        else
        {
            soundMaker.DenyBuyItemSound(transform.position);
        }
    }
}
