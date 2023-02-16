using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollFence : MonoBehaviour
{
    [SerializeField] private float force;
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
        player.KnockBack(Vector2.down, force);
    }
    private void OnPlayerUnsense(Player player)
    {

    }
}
