using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling : MonoBehaviour
{
    [SerializeField] private int currentScaling;
    [SerializeField] private float growthHPRate = 0.35f;
    [SerializeField] private float growthDmgRate = 0.5f;
    public static Scaling instance;
    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if(currentScaling <= 0)
        {
            currentScaling = 1;
        }
    }


    public int SendScaling()
    {
        return currentScaling;
    }

    public void ScalingIncrease()
    {
        currentScaling++;
    }

    public float CalculateHealthOnScaling(float baseHP)
    {
        return Mathf.Pow(baseHP, currentScaling * growthHPRate) + baseHP;
    }

    public float CalculateDamageOnScaling(float baseDmg)
    {
        return Mathf.Pow(baseDmg, currentScaling * growthDmgRate) + baseDmg;
    }
}
