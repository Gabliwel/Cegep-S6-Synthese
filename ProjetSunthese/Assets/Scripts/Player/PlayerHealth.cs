using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth;

    [Header("To show in inspector")]
    [SerializeField] private float currentMax;
    [SerializeField] private float currentHealth;
    [SerializeField] private float armor;

    private bool secondChance = false;

    private void Awake()
    {
        
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

    protected bool Harm(float damageValue)
    {
        return true;
    }
}
