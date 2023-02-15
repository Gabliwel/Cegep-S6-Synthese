using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Utils;

public class PlayerBaseWeaponStat : MonoBehaviour
{
    [Header("Base readable stats")]
    [ReadOnlyAttribute, SerializeField] private float baseAttackMultiplicator = 1;
    [ReadOnlyAttribute, SerializeField] private int baseSpeedLevel = 1;
    [ReadOnlyAttribute, SerializeField] private int poisonLevel = 0;

    #region Set
    public void DoubleBaseAttack()
    {
        // for special item
        baseAttackMultiplicator *= 2;
    }

    public void IncreaseBaseAttack()
    {
        // so, we increase the base attack at a stable 15%
        baseAttackMultiplicator += 0.15f;
    }

    public void IncreaseSpeedLevel(int increaseValue)
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

    public int GetBaseSpeedLevel()
    {
        return baseSpeedLevel;
    }

    public int GetPoisonLevel()
    {
        return poisonLevel;
    }
    #endregion
}
