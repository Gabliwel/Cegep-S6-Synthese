using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnnemy : Enemy
{
    [SerializeField] private float speed = 3;

    private Sensor damageSensor;
    private Sensor rangeSensor;
    private ISensor<Player> playerDamageSensor;
    private ISensor<Player> playerRangeSensor;

    private Player player;

    private bool shootingRange = false;

    private GameObject projectile;

    void Awake()
    {
        rangeSensor = transform.Find("RangeSensor").GetComponent<Sensor>();
        damageSensor = transform.Find("DamageSensor").GetComponent<Sensor>();
        projectile = transform.Find("Fire").gameObject;

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
        shootingRange = true;
    }

    void OnPlayerRangeUnsense(Player player)
    {
        shootingRange = false;
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
        if (!shootingRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            if (!projectile.activeSelf)
            {
                Projectile fireBall = projectile.GetComponent<Projectile>();
                projectile.transform.position = transform.position;
                projectile.SetActive(true);
                fireBall.SetDamage(damageDealt, 0);
                fireBall.SetDestination(player.transform.position);
            }
        }
    }

    protected override void Drop()
    {
        Player.instance.GainDrops(0, xpGiven, goldDropped);
    }
}
