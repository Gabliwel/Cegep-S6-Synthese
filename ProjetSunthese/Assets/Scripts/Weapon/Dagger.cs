using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Melee
{
    public void Slash()
    {
        if (cooldownTimer <= 0)
            StartCoroutine(Attack());
    }

    public void StopOrbit()
    {
        orbit = false;
    }
    //prevent left click from attacking
    public override void StartAttack()
    {
    }

    public void SetStats()
    {

    }
}
