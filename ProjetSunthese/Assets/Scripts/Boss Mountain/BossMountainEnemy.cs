using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMountainEnemy : Enemy
{
    [SerializeField] private float speed = 0.01f;
    [SerializeField] private float damage;
    [SerializeField] private bool shaking;
    [SerializeField] private float shakeTime;
    [SerializeField] private float shakeTimer;
    [SerializeField] private bool isMoving;
    private Player player;
    private Animator animator;
    private Sensor sensor;
    private ISensor<Player> playerSensor;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
        particleScale = 4;
        particleColor = new Color(163, 167, 194);
    }

    void OnPlayerSense(Player player)
    {
        player.Harm(damage);
    }

    void OnPlayerUnsense(Player player)
    {

    }

    private void OnEnable()
    {
        isMoving = false;
        animator.SetBool("Shake", true);
        shaking = true;
        shakeTimer = shakeTime;
        hp = 5;
    }
    protected override void Drop()
    {
    }

    public override void Harm(float ammount, float overtime)
    {
        base.Harm(ammount, overtime);

        Die();
    }

    public void MountainBossSpawned()
    {
        isMoving = true;
    }

    private void Update()
    {
        if (isMoving)
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);

        if (shakeTimer > 0)
            shakeTimer -= Time.deltaTime;
        else if (shaking)
        {
            shaking = false;
            animator.SetBool("Shake", false);
            animator.SetTrigger("Spawn");
        }
    }

}
