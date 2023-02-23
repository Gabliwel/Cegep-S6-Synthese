using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnnemy : Enemy
{
    [SerializeField] private float speed = 3;
    [SerializeField] private GameObject fireBall;

    private Sensor damageSensor;
    private Sensor rangeSensor;
    private Sensor visionSensor;
    private ISensor<Player> playerDamageSensor;
    private ISensor<Player> playerRangeSensor;
    private ISensor<Player> playerVisionSensor;

    private Player player;

    private bool shootingRange = false;

    private GameObject projectile;

    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        rangeSensor = transform.Find("RangeSensor").GetComponent<Sensor>();
        damageSensor = transform.Find("DamageSensor").GetComponent<Sensor>();
        visionSensor = transform.Find("VisionSensor").GetComponent<Sensor>();
        projectile = Instantiate(fireBall);
        projectile.SetActive(false);

        playerRangeSensor = rangeSensor.For<Player>();
        playerDamageSensor = damageSensor.For<Player>();
        playerVisionSensor = visionSensor.For<Player>();

        playerRangeSensor.OnSensedObject += OnPlayerRangeSense;
        playerRangeSensor.OnUnsensedObject += OnPlayerRangeUnsense;

        playerDamageSensor.OnSensedObject += OnPlayerDamageSense;
        playerDamageSensor.OnUnsensedObject += OnPlayerDamageUnsense;

        playerVisionSensor.OnSensedObject += OnPlayerVisionSense;
        playerVisionSensor.OnUnsensedObject += OnPlayerVisionUnsense;

        animator = GetComponent<Animator>();
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

    void OnPlayerVisionSense(Player player)
    {
        this.player = player;
    }

    void OnPlayerVisionUnsense(Player player)
    {
        this.player = null;
    }

    void Update()
    {
        if (player != null)
        {
            animator.SetFloat("Move X", player.transform.position.x - transform.position.x);
            animator.SetFloat("Move Y", player.transform.position.y - transform.position.y);

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
    }

    protected override void Drop()
    {
        Player.instance.GainDrops(0, xpGiven, goldDropped);
    }
}
