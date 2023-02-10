using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaShockWaveController : MonoBehaviour
{
    Vector3 originalScale;
    LavaShockWaveMaskController maskController;
    private Sensor sensor;
    private ISensor<Player> playerSensor;
    [SerializeField] private float damage;


    void Start()
    {
        originalScale = transform.localScale;
        maskController = GetComponentInChildren<LavaShockWaveMaskController>();
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
    }

    void Update()
    {
        transform.localScale += transform.localScale * Time.deltaTime/3;
    }

    void OnPlayerSense(Player player)
    {
        if (!maskController.playerIsInSafeZone)
        {
            player.Harm(damage);
        }
    }


    void OnPlayerUnsense(Player player)
    {
    }

    private void OnDisable()
    {
        ShrinkBackToOriginal();
    }

    private void ShrinkBackToOriginal()
    {
        transform.localScale = originalScale;
    }
}
