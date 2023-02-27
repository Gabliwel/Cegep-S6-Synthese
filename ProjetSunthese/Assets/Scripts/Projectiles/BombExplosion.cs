using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : Explosion
{
    private BombHolder parentHolder;
    private ISensor<Player> playerSensor;
    protected override void Awake()
    {
        base.Awake();
        parentHolder = GetComponentInParent<BombHolder>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnEnemySense;
        playerSensor.OnUnsensedObject += OnEnemyUnsense;
    }

    private void OnEnemySense(Player player)
    {
        player.Harm(damage);
    }

    private void OnEnemyUnsense(Player player)
    {

    }

    public void SetDamage(float ammount)
    {
        damage = ammount;
    }

    public void EndExplosion()
    {
        gameObject.SetActive(false);
        parentHolder.ExplosionFinished();
    }

    private void Update()
    {
        
    }
}
