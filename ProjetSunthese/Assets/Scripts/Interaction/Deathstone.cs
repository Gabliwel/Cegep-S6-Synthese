using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deathstone : Interactable
{
    private void Start()
    {
        int numDeath = AchivementManager.instance.GetNumOfDeath();
        if(numDeath <= 0)
        {
            desc = "Wait, you've never died? You're not supposed to be here...";
        }
        else if(numDeath == 1)
        {
            desc = "You died 1 time";
        }
        else
        {
            desc = "You've died " + numDeath + " times";
        }

    }

    // NO REACTION, ONLY MANAGE DESC FOR DEATH PANEL
}
