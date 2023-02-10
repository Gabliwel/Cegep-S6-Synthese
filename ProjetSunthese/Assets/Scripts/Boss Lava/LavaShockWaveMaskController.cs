using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaShockWaveMaskController : MonoBehaviour
{
    public bool playerIsInSafeZone = false;
    private Sensor sensor;
    private ISensor<Player> playerSensor;
    [SerializeField] private float damage;

    void Start()
    {
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
    }

    void Update()
    {
        
    }

    void OnPlayerSense(Player player)
    {
        playerIsInSafeZone = true;
    }


    void OnPlayerUnsense(Player player)
    {
        playerIsInSafeZone = false;
    }
}
