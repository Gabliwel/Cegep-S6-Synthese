using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrerBallExplosion : Explosion
{
    private BossBofrerBall parentBall;
    private ISensor<Player> playerSensor;

    protected override void Awake()
    {
        base.Awake();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
        parentBall = GetComponentInParent<BossBofrerBall>();
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
        {
            gameObject.SetActive(false);
            parentBall.ExplosionFinished();
        }
    }
}
