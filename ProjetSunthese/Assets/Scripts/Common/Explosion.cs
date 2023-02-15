using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float duration;
    [SerializeField] protected float timer;
    private Sensor sensor;
    private ISensor<Player> playerSensor;

    protected virtual void Awake()
    {
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
        damage = Scaling.instance.CalculateDamageOnScaling(damage);
    }

    private void OnPlayerSense(Player player)
    {
        player.Harm(damage);
    }
    private void OnPlayerUnsense(Player player)
    {

    }

    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        timer = duration;
    }
}
