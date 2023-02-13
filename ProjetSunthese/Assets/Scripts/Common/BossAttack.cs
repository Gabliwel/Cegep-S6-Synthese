using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{
    public bool tempBossAttackReady;
    public abstract void Launch();
}
