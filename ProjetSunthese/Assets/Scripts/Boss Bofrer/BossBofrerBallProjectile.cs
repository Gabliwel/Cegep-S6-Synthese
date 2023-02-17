using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrerBallProjectile : Projectile
{
    private BossBofrerBall parentBall;
    private GameObject player;
    private Sensor sensor;
    private ISensor<Player> playerSensor;

    private void Awake()
    {
        parentBall = GetComponentInParent<BossBofrerBall>();
        player = GameObject.FindGameObjectWithTag("Player");
        destination = player.transform.position;
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnSense;
        damage = Scaling.instance.CalculateDamageOnScaling(damage);
    }

    protected override void Update()
    {
        destination = player.transform.position;
        base.Update();
    }

    private void OnPlayerSense(Player player)
    {
        if (player.Harm(damage))
            DestinationReached();
    }
    private void OnPlayerUnSense(Player player)
    {

    }

    protected override void DestinationReached()
    {
        parentBall.ProjectileReached();
        base.DestinationReached();
    }
}
