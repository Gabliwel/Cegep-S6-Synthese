using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyExplosion : Explosion
{
    private ISensor<Player> playerSensor;
    protected override void Awake()
    {
        base.Awake();

        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnEnemySense;
        playerSensor.OnUnsensedObject += OnEnemyUnsense;
    }
    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            gameObject.SetActive(false);
        }
    }
    private void OnEnemySense(Player player)
    {
        player.Harm(damage);
    }

    private void OnEnemyUnsense(Player player)
    {

    }
}
