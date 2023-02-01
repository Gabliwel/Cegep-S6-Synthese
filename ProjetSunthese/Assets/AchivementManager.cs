using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchivementManager : MonoBehaviour
{
    AchivementClass achivementData;
    private bool openedTwentyFiveChests = false;
    private int currentOpened = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenedChest()
    {
        currentOpened++;
        if(currentOpened == 25)
        {
            openedTwentyFiveChests = true;
        }
    }
}
