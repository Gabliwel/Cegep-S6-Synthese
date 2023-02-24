using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Rarity;

public class ChestGameObject : Interactable
{
    private ChestDropManager chestDropManager;
    private string chestLayer;

    public override void Interact(Player player) 
    {
        ItemWithRarity item = chestDropManager.SendRandomItem();
        GameObject drop = item.item;
        Debug.Log(item.currentRarity);
        drop.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        drop.GetComponent<GenericItem>().SetItem(chestLayer, item.currentRarity);
        drop.SetActive(true);

        gameObject.SetActive(false);
    }

    public void SetChest(ChestDropManager dropManager, string layer)
    {
        chestDropManager = dropManager;
        chestLayer = layer;
        gameObject.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer(layer);
        sprite.sortingLayerName = layer;
        gameObject.layer = LayerMask.NameToLayer(layer);
    }
}
