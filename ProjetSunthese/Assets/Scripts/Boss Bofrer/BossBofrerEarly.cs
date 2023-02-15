using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrerEarly : Enemy
{
    [SerializeField] private float speed;
    [SerializeField] private float HPTreshold;
    private Player player;
    private Animator animator;
    private Sensor sensor;
    private ISensor<Player> playerSensor;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        animator.SetTrigger("Early");
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnSense;
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
        if(hp < HPTreshold)
        {
            GameManager.instance.SetNextLevel();
        }
    }

}
