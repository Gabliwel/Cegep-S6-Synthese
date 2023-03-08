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
        drop.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        GenericItem genItem = drop.GetComponent<GenericItem>();
        genItem.SetItem(chestLayer, item.currentRarity);
        genItem.DoDust();
        drop.SetActive(true);

        AchivementManager.instance.OpenedChest();

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
