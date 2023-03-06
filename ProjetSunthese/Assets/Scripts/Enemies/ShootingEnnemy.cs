using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnnemy : Enemy
{
    [SerializeField] private float speed = 3;
    [SerializeField] private GameObject fireBall;

    private Sensor damageSensor;
    private PlayerProximitySensor rangeSensor;
    private PlayerProximitySensor visionSensor;

    private ISensor<Player> playerDamageSensor;

    private bool shootingDelayGoing = false;

    private GameObject projectile;

    private Animator animator;

    private float SHOOTING_WAIT = 1.5f;

    protected override void Awake()
    {
        base.Awake();
        rangeSensor = transform.Find("RangeSensor").GetComponent<PlayerProximitySensor>();
        damageSensor = transform.Find("DamageSensor").GetComponent<Sensor>();
        visionSensor = transform.Find("VisionSensor").GetComponent<PlayerProximitySensor>();
        projectile = Instantiate(fireBall);
        projectile.SetActive(false);

        playerDamageSensor = damageSensor.For<Player>();

        playerDamageSensor.OnSensedObject += OnPlayerDamageSense;
        playerDamageSensor.OnUnsensedObject += OnPlayerDamageUnsense;

        animator = GetComponent<Animator>();
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
        if (visionSensor.IsClose())
        {
            animator.SetFloat("Move X", Player.instance.transform.position.x - transform.position.x);
            animator.SetFloat("Move Y", Player.instance.transform.position.y - transform.position.y);

            if (!rangeSensor.IsClose())
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.instance.transform.position, speed * Time.deltaTime);
            }
            else
            {
                if (!projectile.activeSelf && !shootingDelayGoing)
                {
                    StartCoroutine(ShootingDelay());
                    SoundMaker.instance.GontrandShockWaveSound(gameObject.transform.position);
                    Projectile fireBall = projectile.GetComponent<Projectile>();
                    projectile.transform.position = transform.position;
                    projectile.SetActive(true);
                    fireBall.SetDamage(damageDealt, 0);
                    fireBall.SetDestination(Player.instance.transform.position);
                }
            }
        }
    }

    protected override void Drop()
    {
        Player.instance.GainDrops(xpGiven, goldDropped);
    }
    private IEnumerator ShootingDelay()
    {
        shootingDelayGoing = true;
        yield return new WaitForSeconds(SHOOTING_WAIT);
        shootingDelayGoing = false;
    }
}
