using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomstone : Interactable
{
    [Header("Action")]
    [SerializeField] private bool replay = false;
    [SerializeField] private bool menu = false;

    public override void Interact(Player player) 
    {
        if(replay)
        {
            Debug.Log("Replay");
            // changer scene de tuto
        }
        else if(menu)
        {
            Debug.Log("Menu");
            // changer scene de menu
        }
    }
}
