using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Daggers : Weapon
{
    [SerializeField] private float delay;
    [SerializeField] private float duration;
    [Range(0, 45)]
    [SerializeField] private float windUpDistance;
    [Range(0, 45)]
    [SerializeField] private float recoilDistance;
    [SerializeField] private Dagger dagger1;
    [SerializeField] private Dagger dagger2;

    protected override void Start()
    {
        base.Start();
        dagger1.transform.parent.position = rotationPoint.position;
        dagger2.transform.parent.position = rotationPoint.position;
        orbit = false;  
    }

    protected override IEnumerator Attack()
    {
        cooldownTimer = cooldown + duration + startup + recovery + delay;
        dagger1.Slash();
        dagger2.StopOrbit();
        yield return new WaitForSeconds(delay);
        dagger2.Slash();
        yield return new WaitForSeconds(recovery);
    }

    public override void SetPlayerBaseWeaponStat(PlayerBaseWeaponStat playerBaseWeaponStat)
    {
        base.SetPlayerBaseWeaponStat(playerBaseWeaponStat);
        dagger1.SetPlayerBaseWeaponStat(playerBaseWeaponStat);
        dagger2.SetPlayerBaseWeaponStat(playerBaseWeaponStat);
    }

    public override void CalculateNewSpeed()
    {
        base.CalculateNewSpeed();
        dagger1.CalculateNewSpeed();
        dagger2.CalculateNewSpeed();
    }

    protected override void EndAttackSpecifics()
    {
    }
}
