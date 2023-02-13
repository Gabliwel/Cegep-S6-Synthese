using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttack : MonoBehaviour
{
    protected bool isAvailable = true;
    public abstract void Launch();
    public bool IsAvailable() { return isAvailable; }
}
