using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMountainRock : BossAttack
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 destination;
    [SerializeField] private bool moving;
    [SerializeField] private bool breaking;

    private BossMountain bossMountain;
    private Animator animator;
    private Sensor sensor;
    private ISensor<Player> playerSensor;
    private Player player;

    private void Awake()
    {
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
        bossMountain = GetComponentInParent<BossMountain>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameObject.SetActive(false);
        type = BossAttackType.Mountain;
        damage = Scaling.instance.CalculateDamageOnScaling(damage);
    }
    private void OnEnable()
    {
        moving = false;
        animator.SetTrigger("Formation");
    }
    public override void Launch()
    {
        gameObject.SetActive(true);
    }

    void OnPlayerSense(Player player)
    {
        if (player.Harm(damage))
            BreakRock();
    }

    void OnPlayerUnsense(Player player)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
        if (DestinationReached() && !breaking)
        {
            BreakRock();
        }
    }

    bool DestinationReached()
    {
        return (Vector2)transform.position == destination;
    }

    void BreakRock()
    {
        moving = false;
        animator.SetBool("Spin", false);
        animator.SetTrigger("Break");
        breaking = true;
    }

    public void Formed()
    {
        animator.SetBool("Spin", true);
        moving = true;
        destination = player.transform.position;
    }

    public void RockBreakEnd()
    {
        breaking = false;
        gameObject.SetActive(false);
    }

    
}
