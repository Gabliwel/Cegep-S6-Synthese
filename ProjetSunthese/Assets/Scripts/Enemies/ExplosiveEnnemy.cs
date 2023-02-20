using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveEnnemy : Enemy
{
    [SerializeField] private float speed = 3;

    private Sensor damageSensor;
    private Sensor rangeSensor;
    private ISensor<Player> playerDamageSensor;
    private ISensor<Player> playerRangeSensor;

    private Player player;
    private ExplosiveEnnemyHolder parentHolder;

    void Awake()
    {
        rangeSensor = transform.Find("RangeSensor").GetComponent<Sensor>();
        damageSensor = transform.Find("DamageSensor").GetComponent<Sensor>();
        parentHolder = GetComponentInParent<ExplosiveEnnemyHolder>();

        playerRangeSensor = rangeSensor.For<Player>();
        playerDamageSensor = damageSensor.For<Player>();

        playerRangeSensor.OnSensedObject += OnPlayerRangeSense;
        playerRangeSensor.OnUnsensedObject += OnPlayerRangeUnsense;

        playerDamageSensor.OnSensedObject += OnPlayerDamageSense;
        playerDamageSensor.OnSensedObject += OnPlayerDamageUnsense;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnPlayerRangeSense(Player player)
    {
        parentHolder.DestinationReached();
    }

    void OnPlayerRangeUnsense(Player player)
    {

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
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    protected override void Drop()
    {
        Player.instance.GainDrops(0, xpGiven, goldDropped);
    }
}
