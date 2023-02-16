using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchManager : MonoBehaviour
{
    private GameObject[] weaponDrops;
    void Start()
    {
        int totalWeapon = transform.childCount;
        weaponDrops = new GameObject[totalWeapon];

        for (int i = 0; i < totalWeapon; i++)
        {
            weaponDrops[i] = transform.GetChild(i).gameObject;
            weaponDrops[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchWeaponOnGround(int weapon, Vector3 dropPosition)
    {
        weaponDrops[weapon - 1].transform.position = dropPosition;
        weaponDrops[weapon - 1].SetActive(true);
    }
}
