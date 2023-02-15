using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    private Sensor sensor;
    private ISensor<Enemy> enemySensor;
    private void Awake()
    {
        sensor = GetComponentInChildren<Sensor>();
        enemySensor = sensor.For<Enemy>();
        enemySensor.OnSensedObject += OnEnemySense;
        enemySensor.OnUnsensedObject += OnEnemyUnsense;
    }

    private void OnEnemySense(Enemy enemy)
    {
        enemy.Harm(damage, poisonDamage);
        gameObject.SetActive(false);
    }

    private void OnEnemyUnsense(Enemy enemy)
    {

    }
}
