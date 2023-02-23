using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Rarity;
using System;

public abstract class GenericItem : Interactable
{
    private ItemRarity rarity;
    private SpriteRenderer rarityLight;

    // Colors for item light base on rarity
    private Color commun = new Color(0.2f, 1, 0.29f, 1);
    private Color rare = new Color(0.1f, 0.61f, 1, 1);
    private Color epic = new Color(0.76f, 0.1f, 1, 1);
    private Color legendary = new Color(1, 0.7f, 0.09f, 1);
    private Color unique = new Color(1, 0.2f, 0.09f, 1);

    protected override void Awake()
    {
        base.Awake();
        rarityLight = gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        SetRarity(ItemRarity.COMMUN);
    }

    public override void Interact(Player player) 
    {
        AddToPlayer(player);
        Collected(player);
    }

    protected abstract void AddToPlayer(Player player);

    public override void ChangeSelectedState(bool selected, DescriptionBox descBox)
    {
        if (selected)
        {
            sprite.material = selectedMaterial;
            if (useTextBox) descBox.PopUp(desc, rarity);
        }
        else
        {
            sprite.material = defaultMaterial;
            if (useTextBox) descBox.Close();
        }
    }

    private void Collected(Player player)
    {
        player.UpdateInteractables(this);
        gameObject.SetActive(false);
    }

    protected void SetRarity(ItemRarity rarity)
    {
        this.rarity = rarity;
        UpdateColorByRarity();
    }

    public void SetItem(string chestLayer, ItemRarity newRarity)
    {
        gameObject.layer = LayerMask.NameToLayer(chestLayer);
        gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer(chestLayer); // for stimuli
        if (sprite == null) Awake();
        sprite.sortingLayerName = chestLayer;
        rarityLight.sortingLayerName = chestLayer;
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        SetRarity(newRarity);
    }

    protected void UpdateColorByRarity()
    {
        Debug.Log(rarity);
        Debug.Log(rarityLight);
        switch (rarity)
        {
            case ItemRarity.COMMUN:
                rarityLight.color = commun;
                break;
            case ItemRarity.RARE:
                rarityLight.color = rare;
                break;
            case ItemRarity.EPIC:
                rarityLight.color = epic;
                break;
            case ItemRarity.LEGENDARY:
                rarityLight.color = legendary;
                break;
            case ItemRarity.UNIQUE:
                rarityLight.color = unique;
                break;
        }
    }
}
