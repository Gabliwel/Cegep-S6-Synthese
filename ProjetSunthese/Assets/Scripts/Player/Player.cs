using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerAnimationController animationController;
    private PlayerMovement playerMovement;
    private Weapon weapon;
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


    [SerializeField] int xp;
    [SerializeField] int level;
    private void Awake()
    {
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

    public void GainXp(int amount)
    {
        xp += amount;
        if(xp >= 100)
        {
            xp -= 100;
            level++;
            weapon.GainLevelDamage();
            MAX_HEALTH += 10;
        }
    }
}
