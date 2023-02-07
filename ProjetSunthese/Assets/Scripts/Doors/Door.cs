using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameObject wall;
    private GameObject door;

    void Awake()
    {
        //à changer au besoin, ou pour être plus clean
        wall = gameObject.transform.GetChild(0).gameObject;
        door = gameObject.transform.GetChild(1).gameObject;

        wall.SetActive(true);
        door.SetActive(false);
    }

    public void ActivateDoor()
    {
        wall.SetActive(false);
        door.SetActive(true);
    }
}
