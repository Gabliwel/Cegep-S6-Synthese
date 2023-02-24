using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrerEarly : Enemy
{
    [SerializeField] private float speed;
    private float hpThreshold;
    private const float SCALING_CUTOFF = 0.2f;
    private Player player;
    private Animator animator;
    private Sensor sensor;
    private ISensor<Player> playerSensor;
    private HPBar hpBar;

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        animator.SetTrigger("Early");
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnSense;
        hpBar = GetComponentInChildren<HPBar>();
    }

    private void Start()
    {
        hpThreshold = CalculateHpThreshold();
    }
    protected override void Drop()
    {
    }


    private void OnPlayerSense(Player player)
    {
        player.Harm(damageDealt);
    }
    private void OnPlayerUnSense(Player player)
    {

    }

    public override void Harm(float ammount, float overtimeDamage)
    {
        base.Harm(ammount, overtimeDamage);
        hpBar.UpdateHp(hp, Scaling.instance.CalculateHealthOnScaling(baseHP));
        CheckHPForTeleport();
    }

    protected override void WasPoisonHurt()
    {
        base.WasPoisonHurt();
        hpBar.UpdateHp(hp, Scaling.instance.CalculateHealthOnScaling(baseHP));
        CheckHPForTeleport();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        animator.SetFloat("MoveX", player.transform.position.x - transform.position.x);
        animator.SetFloat("MoveY", player.transform.position.y - transform.position.y);
    }

    private void CheckHPForTeleport()
    {
        if (hp < hpThreshold)
        {
            GameManager.instance.SetNextLevel();
        }
    }

    private float CalculateHpThreshold()
    {
        return hp - (hp * SCALING_CUTOFF);
    }

}
