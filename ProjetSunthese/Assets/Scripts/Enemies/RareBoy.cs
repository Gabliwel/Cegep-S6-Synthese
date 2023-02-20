using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareBoy : Enemy
{
    [SerializeField] private float speed = 5;

    private Sensor damageSensor;
    private Sensor rangeSensor;
    private ISensor<Player> playerRangeSensor;

    private Player player;

    void Awake()
    {
        rangeSensor = transform.Find("RangeSensor").GetComponent<Sensor>();

        playerRangeSensor = rangeSensor.For<Player>();

        playerRangeSensor.OnSensedObject += OnPlayerRangeSense;
        playerRangeSensor.OnUnsensedObject += OnPlayerRangeUnsense;
    }

    void OnPlayerRangeSense(Player player)
    {
        this.player = player;
    }

    void OnPlayerRangeUnsense(Player player)
    {
        this.player = null;
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 reversed = new Vector2();
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    protected override void Drop()
    {
        Player.instance.GainDrops(0, xpGiven, goldDropped);
    }
}
