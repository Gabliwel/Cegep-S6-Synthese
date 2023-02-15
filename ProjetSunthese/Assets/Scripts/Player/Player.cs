using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Weapons;
using Billy.Utils;

public class Player : MonoBehaviour
{
    private PlayerAnimationController animationController;
    private PlayerMovement playerMovement;
    private Weapon weapon;
    private WeaponInformations weaponInfo;
    private Inventory inventory;
    private PlayerLight playerLight;
    private ProjectilesManager projectilesManager;
    private PlayerHealth health;
    private PlayerBaseWeaponStat baseWeaponStat;
    private SpriteRenderer sprite;
    private float iframesTimer;

    [Header("Link")]
    [SerializeField] private GameObject stimuli;
    [SerializeField] private GameObject sensor;

    [Header("Ressources")]
    [ReadOnlyAttribute, SerializeField] private int gold = 0;
    [ReadOnlyAttribute, SerializeField] private float levelUpAugmentationRate = 1.4f;
    [ReadOnlyAttribute, SerializeField] private int neededXp = 100;
    [ReadOnlyAttribute, SerializeField] private int currentXp = 0;
    [ReadOnlyAttribute, SerializeField] private int level = 1;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animationController = GetComponent<PlayerAnimationController>();
        playerMovement = GetComponent<PlayerMovement>();
        weapon = GetComponentInChildren<Weapon>();
        weaponInfo = weapon.gameObject.GetComponent<WeaponInformations>();
        playerLight = GetComponentInChildren<PlayerLight>();
        health = GetComponent<PlayerHealth>();
        baseWeaponStat = GetComponent<PlayerBaseWeaponStat>();
    }

    private void Start()
    {
        weapon.SetPlayerBaseWeaponStat(baseWeaponStat);
        weapon.CalculateNewSpeed();
        animationController.ChangeOnWeaponType(weaponInfo.GetWeaponType());
    }

    private void Update()
    {
        if (iframesTimer > 0)
            iframesTimer -= Time.deltaTime;
    }

    public void AddIframes(float ammount)
    {
        iframesTimer += ammount;
    }


    #region Health
    public void MaxHealthBoost(float value)
    {
        health.AddMaxHp(value);
    }

    public void Heal(float healingAmount)
    {
        health.Heal(healingAmount);
    }

    public void GainArmor(float value)
    {
        health.GainArmor(value);
    }

    public void GainStoneHeart()
    {
        health.GainStoneHeart();
    }
    #endregion

    public void GetCrazyHalfHeart()
    {
        health.IncreaseReceiveDamageMultiplicator();
        baseWeaponStat.DoubleBaseAttack();
    }

    public void GainBloodSuck()
    {
        //bloodSuck = true;
    }

    public void HealBloodSuck(int amount)
    {
        /*
        if (bloodSuck)
        {
            Heal(amount);
        }
        */
    }

    public void GainXp(int amount)
    {
        currentXp += amount;
        if (currentXp >= neededXp)
        {
            currentXp -= neededXp;
            neededXp = (int)Math.Round(neededXp * levelUpAugmentationRate);
            level++;
            BoostDamage();
            MaxHealthBoost(10);
        }
    }

    public void BoostDamage()
    {
        baseWeaponStat.IncreaseBaseAttack();
    }

    public void IncreaseAttackSpeed(int lvl)
    {
        baseWeaponStat.IncreaseSpeedLevel(lvl);
        weapon.CalculateNewSpeed();
    }

    public void AddPoison(int lvl)
    {
        baseWeaponStat.IncreasePoisonLevel(lvl);
    }

    public void GainGold(int amount)
    {
        gold += amount;
    }

    public void GainDrops(int health, int xp, int gold)
    {
        HealBloodSuck(health);
        GainXp(xp);
        GainGold(gold);
    }

    public bool BuyItem(int price)
    {
        if (gold >= price)
        {
            gold -= price;
            return true;
        }
        return false;
    }

    public bool Harm(float ammount)
    {
        if(iframesTimer <= 0)
        {
            health.Harm(ammount);
            return true;
        }
        return false;
    }

    public void ChangeLayer(string layer, string sortingLayer)
    {
        gameObject.layer = LayerMask.NameToLayer(layer);
        stimuli.layer = LayerMask.NameToLayer(layer);
        sensor.layer = LayerMask.NameToLayer(layer);
        sprite.sortingLayerName = sortingLayer;
        weaponInfo.ChangeLayer(layer, sortingLayer);
        playerLight.UpdateLightUsage(sortingLayer);
    }

    public void BlocMovement(bool state)
    {
        if(state) playerMovement.DisableMovement();
        else playerMovement.EnableMovement();
    }

    public void BlocAttack(bool state)
    {
        weapon.gameObject.SetActive(!state);
    }

    public void SetProjectilesManager(ProjectilesManager newProjectilesManager)
    {
        projectilesManager = newProjectilesManager;
        SetCurrentWeapon();
    }

    private void SetCurrentWeapon()
    {
        if(weaponInfo.GetWeaponType() == WeaponsType.BOW)
        {
            weapon.gameObject.GetComponent<Ranged>().SetProjectiles(projectilesManager.GetArrows());
        }
    }

    public void SwitchWeapon(GameObject newWeapon)
    {
        Vector3 originalPosition = newWeapon.transform.position;

        weapon.EndAttack();

        Transform weaponParent = weapon.transform.parent;
        weapon.transform.parent = null;

        WeaponInformations oldWeaponInfo = weapon.GetComponent<WeaponInformations>();
        oldWeaponInfo.gameObject.transform.position = originalPosition;
        oldWeaponInfo.gameObject.transform.localScale = oldWeaponInfo.GetScaleNotWithPlayer();
        oldWeaponInfo.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        oldWeaponInfo.SwitchToInteractable();

        weapon = newWeapon.GetComponent<Weapon>();

        newWeapon.transform.SetParent(weaponParent.transform, true);
        WeaponInformations newWeaponInfo = newWeapon.GetComponent<WeaponInformations>();
        weaponInfo = newWeaponInfo;

        newWeapon.transform.localPosition = newWeaponInfo.GetPositionWithPlayer();
        newWeapon.transform.localScale = new Vector3(1, 1, 1);
        weapon.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);

        newWeaponInfo.SwitchToWeapon();
        weapon.SetPlayerBaseWeaponStat(baseWeaponStat);
        weapon.SetDefault();

        //change anim et autre...
        animationController.ChangeOnWeaponType(weaponInfo.GetWeaponType());
        SetCurrentWeapon();
    }

    public void KnockBack(Vector2 difference, float force)
    {
        if (iframesTimer <= 0)
        {
            playerMovement.AddKnockBack(difference, force);
        }
    }

    [ContextMenu("NextLevel")]
    public void NextLevel()
    {
        GameManager.instance.GetRandomNextLevelAndStart();
    }

    [ContextMenu("Central")]
    public void BackToMain()
    {
        GameManager.instance.GetBackToMainStageAndStart();
    }
}
