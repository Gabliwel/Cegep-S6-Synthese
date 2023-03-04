using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    private bool isEnemyOccupied = false;
    private bool isPlayerOccupied = false;

    private Sensor sensor;
    private ISensor<Enemy> enemySensor;
    private ISensor<Player> playerSensor;

    void Start()
    {
        sensor = GetComponentInChildren<Sensor>();
        enemySensor = sensor.For<Enemy>();
        playerSensor = sensor.For<Player>();
        enemySensor.OnSensedObject += OnEnemySense;
        enemySensor.OnUnsensedObject += OnEnemyUnsense;
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
    }

    private void OnPlayerUnsense(Player otherObject)
    {
        isPlayerOccupied = false;
    }

    private void OnPlayerSense(Player otherObject)
    {
        isPlayerOccupied = true;
    }

    private void OnEnemySense(Enemy enemy)
    {
        isEnemyOccupied = true;
    }

    private void OnEnemyUnsense(Enemy enemy)
    {
        isEnemyOccupied = false;
    }

    public bool IsOccupied()
    {
        if (isEnemyOccupied || isPlayerOccupied) return true;
        return false;
    }
}
