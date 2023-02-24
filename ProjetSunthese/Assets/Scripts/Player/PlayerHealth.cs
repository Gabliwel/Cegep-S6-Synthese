using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Player player;

    [Header("Health")]
    [SerializeField] private float maxHealth;

    [Header("Info for debug - base")]
    [SerializeField] private float currentMax = 0;
    [SerializeField] private float currentHealth = 0;
    [SerializeField] private float armor = 0;

    public float CurrentHealth { get => currentHealth;}

    [Header("Info for debug - bonus")]
    // ------------- Health bonus --------------------
    // if currentHealth is 15% of max, regen health to 80% of max
    [SerializeField]  private int secondChance = 0;
    // if currentHealth is equivalent to dead, rest alive with 1 hp
    [SerializeField]  private int deathContract = 0;
    // double the amount of lives and gain 10 armor, but cant regen
    [SerializeField]  private bool stoneHeart = false;

    // for player bonus that is independant health
    [SerializeField]  private int receiveDamageMultiplicator = 1;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    void Start()
    {
        currentHealth = maxHealth;
        currentMax = currentHealth;
        armor = 0;
    }

    protected float GetCurrentHealth()
    {
        return currentHealth;
    }

    public void Harm(float damageValue)
    {
        float dammage = (damageValue * receiveDamageMultiplicator) - armor;
        Debug.Log("oof ouch ive been hit for " + (dammage) + " damage");
        if (dammage < 1) dammage = 1; 
        currentHealth -= dammage;

        if (currentHealth <= 0 && deathContract > 0)
        {
            deathContract--;
            currentHealth = 1;
            // do anim or show somehting
            // cant hit for duration?
        }

        if(currentHealth <= currentMax * 0.15f && secondChance > 0)
        {
            secondChance--;
            currentHealth = currentMax * 0.80f;
            // do anim or show somehting
            // cant hit for duration?
        }

        if (currentHealth <= 0)
        {
            // call to player for anim and gamemanager
            GameManager.instance.SetGameOver();
            Debug.Log("I am dead");
        }
    }

    public void HealPercent(float decimalPercent)
    {
        if (!stoneHeart)
        {
            currentHealth += currentMax * decimalPercent;
            if (currentHealth > currentMax)
            {
                currentHealth = currentMax;
            }
        }
    }

    public void HealSpecific(float value)
    {
        if (!stoneHeart)
        {
            currentHealth += value;
            if (currentHealth > currentMax)
            {
                currentHealth = currentMax;
            }
        }
    }

    #region For bonus
    public void GainStoneHeart()
    {
        if(!stoneHeart)
        {
            stoneHeart = true;
            currentHealth *= 2;
            currentMax = currentHealth;
        }
        armor += 5;
    }

    public void GainDeathContract()
    {
        deathContract++;
    }

    public void GainSecondChance()
    {
        secondChance++;
    }

    public void GainArmor(float value)
    {
        armor += value;
    }

    public void LevelUpChange()
    {
        if (stoneHeart) return;
        // add 5% of health
        float toAdd = currentMax * 0.05f;
        AddMaxHp(toAdd);
    }

    public void AddMaxHp(float value)
    {
        if (stoneHeart) return;
        currentMax += value;
        currentHealth += value;
    }

    public void IncreaseReceiveDamageMultiplicator()
    {
        receiveDamageMultiplicator++;
    }
    #endregion
}
