using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public bool isOccupied = false;
    private Sensor sensor;
    private ISensor<Enemy> enemySensor;
    void Start()
    {
        sensor = GetComponentInChildren<Sensor>();
        enemySensor = sensor.For<Enemy>();
        enemySensor.OnSensedObject += OnEnemySense;
        enemySensor.OnUnsensedObject += OnEnemyUnsense;
    }

    void Update()
    {
        
    }

    private void OnEnemySense(Enemy enemy)
    {
        isOccupied = true;
    }

    private void OnEnemyUnsense(Enemy enemy)
    {
        isOccupied = false;
    }
}
