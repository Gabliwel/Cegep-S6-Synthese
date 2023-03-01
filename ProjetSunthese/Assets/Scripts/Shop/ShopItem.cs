using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Billy.Rarity;
using System;

public class ShopItem : Interactable
{
    private GameObject item;
    private SpriteRenderer itemSprite;
    private TMP_Text text;
    private SoundMaker soundMaker;

    private int price;
    private bool boxHasOutline = false;
    private ItemRarity rarity;

    // prices for item with rairty
    private const int priceCommun = 20;
    private const int priceRare = 30;
    private const int priceEpic = 50;
    private const int priceLegendary = 70;
    private const int priceUnique = 100;

    private void Start()
    {
        soundMaker = GameObject.FindGameObjectWithTag("SoundMaker").GetComponent<SoundMaker>();
    }

    public void SetItem(GameObject newItem, int newPrice)
    {
        price = newPrice;
        item = newItem;
        Interactable interactable = newItem.GetComponent<Interactable>();

        title = interactable.Title;
        desc = interactable.Desc;

        itemSprite = item.GetComponent<SpriteRenderer>();
        text = GetComponentInChildren<TMP_Text>();
        itemSprite.sortingOrder = 5;
        text.text = newPrice.ToString();

        gameObject.transform.localEulerAngles = Vector3.zero;
    }

    public void SetItem(GameObject item, int price, ItemRarity newRarity)
    {
        SetItem(item, price);
        boxHasOutline = true;
        rarity = newRarity;

        switch (rarity)
        {
            case ItemRarity.COMMUN:
                this.price = priceCommun;
                break;
            case ItemRarity.RARE:
                this.price = priceRare;
                break;
            case ItemRarity.EPIC:
                this.price = priceEpic;
                break;
            case ItemRarity.LEGENDARY:
                this.price = priceLegendary;
                break;
            case ItemRarity.UNIQUE:
                this.price = priceUnique;
                break;
        }
        text.text = this.price.ToString();
    }

    public override void ChangeSelectedState(bool selected, DescriptionBox descBox)
    {
        if (selected)
        {
            itemSprite.material = selectedMaterial;
            if (UseTextBox)
            {
                if (boxHasOutline) descBox.PopUp(title, desc, rarity);
                else descBox.PopUp(title, desc);
            }
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
            SoundMaker.instance.BuyItemSound(transform.position);
            item.transform.parent = null;
            item.GetComponent<ShopItemMovement>().IsNowSold(itemSprite);
            gameObject.SetActive(false);
            player.UpdateInteractables(this);
        }
        else
        {
            SoundMaker.instance.DenyBuyItemSound(transform.position);
        }
    }
}
