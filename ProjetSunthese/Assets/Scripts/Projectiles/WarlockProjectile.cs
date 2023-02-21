using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockProjectile : Projectile
{
    private WarlockProjectileHolder parentHolder;

    private Sensor sensor;
    private ISensor<Enemy> playerSensor;

    private void Awake()
    {
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Enemy>();
        playerSensor.OnSensedObject += OnEnemySense;
        playerSensor.OnUnsensedObject += OnEnemyUnsense;
        parentHolder = GetComponentInParent<WarlockProjectileHolder>();
    }

    private void OnEnemySense(Enemy enemy)
    {
        enemy.Harm(damage, poisonDamage);
        DestinationReached();
    }
    private void OnEnemyUnsense(Enemy enemy)
    {

    }

    protected override void DestinationReached()
    {
        parentHolder.ProjectileReached();

        base.DestinationReached();
    }
}
