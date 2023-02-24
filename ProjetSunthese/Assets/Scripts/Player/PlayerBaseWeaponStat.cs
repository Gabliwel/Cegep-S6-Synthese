using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseWeaponStat : MonoBehaviour
{
    [Header("Base readable stats")]
    [SerializeField] private float baseAttackMultiplicator = 1;
    [SerializeField] private float baseSpeedLevel = 1;
    [SerializeField] private int poisonLevel = 2;

    #region Set
    public void MultiplyBaseAttack(float value)
    {
        // for special item
        baseAttackMultiplicator *= value;
    }

    public void IncreaseBaseAttack(float value)
    {
        // so, we increase the base attack with decimal percent
        baseAttackMultiplicator += value;
    }

    public void IncreaseSpeedLevel(float increaseValue)
    {
        baseSpeedLevel += increaseValue;
    }

    public void IncreasePoisonLevel(int increaseValue)
    {
        poisonLevel += increaseValue;
    }
    #endregion

    #region Get
    public float GetBaseAttackMultiplicator()
    {
        return baseAttackMultiplicator;
    }

    public float GetBaseSpeedLevel()
    {
        return baseSpeedLevel;
    }

    public int GetPoisonLevel()
    {
        return poisonLevel;
    }
    #endregion
}
