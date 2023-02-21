using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlockExplosion : Explosion
{
    private WarlockProjectileHolder parentHolder;
    private ISensor<Enemy> enemySensor;
    protected override void Awake()
    {
        base.Awake();
        parentHolder = GetComponentInParent<WarlockProjectileHolder>();
        enemySensor = sensor.For<Enemy>();
        enemySensor.OnSensedObject += OnEnemySense;
        enemySensor.OnUnsensedObject += OnEnemyUnsense;
    }

    private void OnEnemySense(Enemy enemy)
    {
        enemy.Harm(damage, 0);
    }
    private void OnEnemyUnsense(Enemy enemy)
    {

    }

    public void SetDamage(float ammount)
    {
        damage = ammount;
    }

    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            gameObject.SetActive(false);
            parentHolder.ExplosionFinished();
        }
    }
}
