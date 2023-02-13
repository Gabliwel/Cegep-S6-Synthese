using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Weapons
{
    public enum WeaponsType
    {
        AXE,
        BOW,
        SWORD,
        DAGUER
    }
}

public class WeaponInformations : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private WeaponsType weaponType = WeaponsType.AXE;
    [SerializeField] private Vector3 scaleWithPlayer;
    [SerializeField] private Vector3 scaleNotWithPlayer;
    [SerializeField] private Vector3 positionWithPlayer;

    [Header("Link")]
    [SerializeField] private bool isCurrentWeapon;
    [SerializeField] private Weapon weapon;
    [SerializeField] private GameObject interactStimuli;
    [SerializeField] private GameObject weaponSensor;

    private void Start()
    {
        if(isCurrentWeapon)
        {
            interactStimuli.SetActive(false);
        }
        else
        {
            weapon.enabled = false;
        }
    }

    public WeaponsType GetWeaponType()
    {
        return weaponType;
    }

    public Vector3 GetScaleWithPlayer()
    {
        return scaleWithPlayer;
    }

    public Vector3 GetScaleNotWithPlayer()
    {
        return scaleNotWithPlayer;
    }

    public Vector3 GetPositionWithPlayer()
    {
        return positionWithPlayer;
    }

    public void SwitchToInteractable()
    {
        isCurrentWeapon = false;
        if (weaponSensor != null) weaponSensor.SetActive(false);
        weapon.enabled = false;
        interactStimuli.SetActive(true);
    }

    public void SwitchToWeapon()
    {
        interactStimuli.SetActive(false);
        weapon.enabled = true;
        if (weaponSensor != null) weaponSensor.SetActive(true);
        isCurrentWeapon = true;
    }
}
