using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerAnimationController animationController;
    private PlayerMovement playerMovement;
    private Weapon weapon;
    private int currentWeapon = 4;
    private float iframesTimer;

    [Header("Health")]
    [SerializeField] float MAX_HEALTH;
    [SerializeField] float currentHealth;

    private int armorBonus = 0;

    [Header("Unique Buff")]
    [SerializeField] bool secondChance;
    [SerializeField] bool deathContract;
    [SerializeField] bool doubleNumber;
    [SerializeField] bool bloodSuck;
    [SerializeField] bool stoneHeart;

    [Header("Ressources")]
    [SerializeField] int gold;
    [SerializeField] int xp;
    [SerializeField] int level;

    [SerializeField] GameObject[] possibleWeapons;
    private WeaponSwitchManager switchWeapon;

    private int poisonDamage = 0;

    private float attackSpeedBoost = 0;
    private int damageBoost = 0;

    private void Awake()
    {
        switchWeapon = GameObject.FindGameObjectWithTag("WeaponSwitch").GetComponent<WeaponSwitchManager>();
        animationController = GetComponent<PlayerAnimationController>();
        playerMovement = GetComponent<PlayerMovement>();
        weapon = GetComponentInChildren<Weapon>();
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

    public void SwitchWeaponType(int weaponNb)
    {
        if(weapon.gameObject.tag == "Melee" && weaponNb == 4)
        {
            weapon.gameObject.SetActive(false);
            possibleWeapons[0].SetActive(true);
            weapon = possibleWeapons[0].GetComponent<Weapon>();
            weapon.SwitchWeapon(weaponNb);
        }
        else if (weapon.gameObject.tag == "Ranged")
        {
            weapon.gameObject.SetActive(false);
            possibleWeapons[1].SetActive(true);
            weapon = possibleWeapons[1].GetComponent<Weapon>();
            weapon.SwitchWeapon(weaponNb);
        }
        else
        {
            weapon.SwitchWeapon(weaponNb);
        }
        switchWeapon.SwitchWeaponOnGround(currentWeapon, transform.position);
        currentWeapon = weaponNb;
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
