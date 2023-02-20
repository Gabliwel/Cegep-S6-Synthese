using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Projectile
{
    private Sensor sensor;
    private ISensor<Player> playerSensor;
    private void Awake()
    {
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
    }

    private void OnPlayerSense(Player player)
    {
        player.Harm(damage);
        gameObject.SetActive(false);
    }

    private void OnPlayerUnsense(Player player)
    {

    }
}
