using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaEnemy : Enemy
{
    [SerializeField] private float speed = 5;

    private Sensor damageSensor;
    private Sensor rangeSensor;
    private ISensor<Player> playerDamageSensor;
    private ISensor<Player> playerRangeSensor;

    private Player player;

    void Awake()
    {
        rangeSensor = transform.Find("RangeSensor").GetComponent<Sensor>();
        damageSensor = transform.Find("DamageSensor").GetComponent<Sensor>();

        playerRangeSensor = rangeSensor.For<Player>();
        playerDamageSensor = damageSensor.For<Player>();

        playerRangeSensor.OnSensedObject += OnPlayerRangeSense;
        playerRangeSensor.OnUnsensedObject += OnPlayerRangeUnsense;

        playerDamageSensor.OnSensedObject += OnPlayerDamageSense;
        playerDamageSensor.OnSensedObject += OnPlayerDamageUnsense;
    }

    void OnPlayerRangeSense(Player player)
    {
        this.player = player;
    }

    void OnPlayerRangeUnsense(Player player)
    {
        this.player = null;
    }

    void OnPlayerDamageSense(Player player)
    {
        player.Harm(10);
    }

    void OnPlayerDamageUnsense(Player player)
    {

    }

    void Update()
    {
        base.Update();
        if(player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    protected override void Drop()
    {
        Player.instance.GainDrops(0, xpGiven, goldDropped);
    }

}
