using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    [Header("Melee")]
    [SerializeField] private float duration;
    [Range(0, 45)]
    [SerializeField] private float windUpDistance;
    [Range(0, 45)]
    [SerializeField] private float recoilDistance;
    private Sensor sensor;
    private ISensor<Enemy> enemySensor;

    protected override void Awake()
    {
        base.Awake();
        sensor = GetComponentInChildren<Sensor>();
        enemySensor = sensor.For<Enemy>();
        enemySensor.OnSensedObject += OnEnemySense;
        enemySensor.OnUnsensedObject += OnEnemyUnsense;
        DeactivateSensor();
    }

    private void OnEnemySense(Enemy enemy)
    {
        if (doubleNumber)
        {
            enemy.Harm(damage * 2, poisonDamage);
        }
        else
        {
            enemy.Harm(damage, poisonDamage);
        }
    }

    private void OnEnemyUnsense(Enemy enemy)
    {
    }

    protected void ActivateSensor()
    {
        sensor.gameObject.SetActive(true);
    }
    protected void DeactivateSensor()
    {
        sensor.gameObject.SetActive(false);
    }
    protected override IEnumerator Attack()
    {
        cooldownTimer = cooldown + duration + startup + recovery;
        orbit = false;
        axis.z += windUpDistance;
        rotationPoint.rotation = Quaternion.Euler(axis);
        yield return new WaitForSeconds(startup);
        float stopTime = Time.time + duration;
        ActivateSensor();
        while (Time.time < stopTime)
        {
            yield return new WaitForEndOfFrame();
            axis.z -= (recoilDistance + windUpDistance) / duration * Time.deltaTime;
            rotationPoint.rotation = Quaternion.Euler(axis);
            yield return null;
        }
        DeactivateSensor();
        yield return new WaitForSeconds(recovery);
        orbit = true;
    }

}
