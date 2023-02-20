using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Weapons;

namespace Billy.Weapons
{
    public enum WeaponsType
    {
        AXE,
        BOW,
        SWORD,
        DAGGER,
        STAFF,
        WARLORCK_STAFF
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
    private SpriteRenderer sprite;
    private bool hasInteracted = false;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if(isCurrentWeapon)
        {
            interactStimuli.SetActive(false);
            gameObject.transform.localScale = new Vector3(1, 1 ,1);
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
        sprite.sortingOrder = 3;
        hasInteracted = true;
    }

    public void SwitchToWeapon()
    {
        interactStimuli.SetActive(false);
        weapon.enabled = true;
        if (weaponSensor != null) weaponSensor.SetActive(true);
        isCurrentWeapon = true;
        sprite.sortingOrder = 9;
        hasInteracted = true;
    }

    public void ChangeLayer(string layer, string sortingLayer)
    {
        gameObject.layer = LayerMask.NameToLayer(layer);
        sprite.sortingLayerName = sortingLayer;
        if (weaponSensor != null) weaponSensor.gameObject.layer = LayerMask.NameToLayer(layer);
        interactStimuli.gameObject.layer = LayerMask.NameToLayer(layer);
    }

    private void OnLevelWasLoaded(int level)
    {
        if(!isCurrentWeapon && hasInteracted)
        {
            Destroy(this.gameObject);
        }
    }
}
