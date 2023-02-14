using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class Player : MonoBehaviour
{
    private PlayerAnimationController animationController;
    private PlayerMovement playerMovement;
    private Weapon weapon;
    private WeaponInformations weaponInfo;
    private Inventory inventory;
    private PlayerLight playerLight;
    private ProjectilesManager projectilesManager;
    private SpriteRenderer sprite;
    private float iframesTimer;

    [Header("Health")]
    [SerializeField] private float MAX_HEALTH;
    [SerializeField] private float currentHealth;

    private int armorBonus = 0;

    [Header("Unique Buff")]
    [SerializeField] private bool secondChance;
    [SerializeField] private bool deathContract;
    [SerializeField] private bool doubleNumber;
    [SerializeField] private bool bloodSuck;
    [SerializeField] private bool stoneHeart;

    [Header("Ressources")]
    [SerializeField] private int gold;
    [SerializeField] private int xp;
    [SerializeField] private int level;

    private int poisonDamage = 0;

    private float attackSpeedBoost = 0;
    private int damageBoost = 0;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animationController = GetComponent<PlayerAnimationController>();
        playerMovement = GetComponent<PlayerMovement>();
        weapon = GetComponentInChildren<Weapon>();
        weaponInfo = weapon.gameObject.GetComponent<WeaponInformations>();
        playerLight = GetComponentInChildren<PlayerLight>();
    }

    private void Start()
    {
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
    public void MaxHealthBoost()
    {
        MAX_HEALTH += 10;
    }

    public void Heal(int healingAmount)
    {
        if (!stoneHeart)
        {
            currentHealth += healingAmount;
            if (currentHealth > MAX_HEALTH)
            {
                currentHealth = MAX_HEALTH;
            }
        }
    }

    public void GainArmor()
    {
        armorBonus += 1;
    }

    public void GetDoubleNumber()
    {
        doubleNumber = true;
        weapon.GainDoubleNumber();
    }

    public bool CheckDouble()
    {
        return doubleNumber;
    }

    public void GainBloodSuck()
    {
        bloodSuck = true;
    }

    public void GainStoneHeart()
    {
        stoneHeart = true;
        MAX_HEALTH *= 2;
        currentHealth *= 2;
    }

    public void HealBloodSuck(int amount)
    {
        if (bloodSuck)
        {
            Heal(amount);
        }
    }

    /// <summary>
    /// harms the player. returns true if it did damage
    /// </summary>
    /// <param name="ammount"></param>
    /// <returns></returns>
    /// 
    public bool Harm(float ammount)
    {
        if(iframesTimer <= 0)
        {
            if (doubleNumber)
            {
                ammount = ammount * 2;
            }

            Debug.Log("oof ouch ive been hit for " + (ammount - armorBonus) + " damage");
            currentHealth -= (ammount - armorBonus);

            if (deathContract && currentHealth <= 0)
            {
                deathContract = false;
                currentHealth = 1;
            }

            if (currentHealth <= (MAX_HEALTH * 15 / 100) && secondChance)
            {
                currentHealth = MAX_HEALTH * 80 / 100;
                secondChance = false;
            }

            if (currentHealth <= 0)
            {
                Debug.Log("I am dead");
            }

            return true;
        }
        return false;
    }

    public void GainDrops(int health, int xp, int gold)
    {
        HealBloodSuck(health);
        GainXp(xp);
        GainGold(gold);
    }

    public void GainXp(int amount)
    {
        xp += amount;
        if(xp >= 100)
        {
            xp -= 100;
            level++;
            damageBoost++;
            MAX_HEALTH += 10;
        }
    }


    public void BoostDamage()
    {
        damageBoost++;
        weapon.AddDamage();
    }

    public int GetDamageBoost()
    {
        return damageBoost;
    }

    public void IncreaseAttackSpeed()
    {
        attackSpeedBoost++;
        weapon.GainSpeed(attackSpeedBoost);
    }

    public float GetAttackSpeed()
    {
        return attackSpeedBoost;
    }

    public void AddPoison()
    {
        poisonDamage += 5;
        weapon.GainPoison();
    }

    public int GetPoisonDamage()
    {
        return poisonDamage;
    }

    public void ChangeLayer(string layer, string sortingLayer)
    {
        gameObject.layer = LayerMask.NameToLayer(layer);
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
        weapon.SetDefault();

        //change anim et autre...
        animationController.ChangeOnWeaponType(weaponInfo.GetWeaponType());
        if(weaponInfo.GetWeaponType() == WeaponsType.BOW)
        {
            weapon.gameObject.GetComponent<Ranged>().SetProjectiles(projectilesManager.GetArrows());
        }
    }

    public void GainGold(int amount)
    {
        gold += amount;
    }

    public bool BuyItem(int price)
    {
        if(gold >= price)
        {
            gold -= price;
            return true;
        }
        return false;
    }

    public void KnockBack(Vector2 difference, float force)
    {
        if (iframesTimer <= 0)
        {
            playerMovement.AddKnockBack(difference, force);
        }
    }
}
