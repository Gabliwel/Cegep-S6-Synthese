using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Interactable
{
    public override void Interact(Player player)
    {
        Debug.Log("Called");
        GameManager.instance.SetNextLevel();
    }
}
