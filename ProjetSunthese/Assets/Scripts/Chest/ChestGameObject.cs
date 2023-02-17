using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestGameObject : Interactable
{
    private ChestDropManager chestDropManager;
    private string chestLayer;

    public override void Interact(Player player) 
    {
        GameObject drop = chestDropManager.SendRandomItem();
        drop.layer = LayerMask.NameToLayer(chestLayer);
        drop.transform.GetChild(0).gameObject.layer = LayerMask.NameToLayer(chestLayer);
        drop.GetComponent<SpriteRenderer>().sortingLayerName = chestLayer;
        drop.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
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
