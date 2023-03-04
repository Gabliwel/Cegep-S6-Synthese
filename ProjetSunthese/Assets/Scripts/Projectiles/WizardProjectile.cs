using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardProjectile : Projectile
{
    [SerializeField] private float tickRate;
    private Sensor sensor;
    private ISensor<Enemy> enemySensor;
    private float tickTimer;
    private void Awake()
    {
        sensor = GetComponentInChildren<Sensor>();
        enemySensor = sensor.For<Enemy>();
        enemySensor.OnSensedObject += OnEnemySense;
        enemySensor.OnUnsensedObject += OnEnemyUnsense;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        tickTimer = tickRate;
    }

    protected override void Update()
    {
        base.Update();

        if (tickTimer > 0)
            tickTimer -= Time.deltaTime;
        else
        {
            tickTimer = tickRate;
            List<Enemy> temp = new List<Enemy>(enemySensor.SensedObjects); // to prevent list was modified exception when enemy dies from tick
            foreach (Enemy enemy in temp)
            {
                enemy.Harm(damage, poisonDamage);
            }
        }
    }

    private void OnEnemySense(Enemy enemy)
    {
    }

    private void OnEnemyUnsense(Enemy enemy)
    {
    }
}
