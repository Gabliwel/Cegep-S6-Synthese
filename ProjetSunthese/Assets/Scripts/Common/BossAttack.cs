using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{
    protected bool isAvailable = true;
    protected BossAttackType type;


    public abstract void Launch();
    public bool IsAvailable() { return isAvailable; }
    public BossAttackType GetBossAttackType() { return type; }
}
