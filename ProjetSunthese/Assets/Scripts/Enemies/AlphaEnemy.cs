using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlphaEnemy : Enemy
{
    [SerializeField] private float speed = 5;

    [SerializeField] bool useNavMesh;

    private Sensor damageSensor;
    private ISensor<Player> playerDamageSensor;
    private bool canDamage = true;
    private bool tickGoing = false;

    private NavMeshAgent agent;
    private Animator animator;
    private PlayerProximitySensor proximitySensor;
    protected override void Awake()
    {
        base.Awake();
        damageSensor = transform.Find("DamageSensor").GetComponent<Sensor>();
        proximitySensor = GetComponent<PlayerProximitySensor>();

        playerDamageSensor = damageSensor.For<Player>();

        playerDamageSensor.OnSensedObject += OnPlayerDamageSense;
        playerDamageSensor.OnUnsensedObject += OnPlayerDamageUnsense;

        if (useNavMesh)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.speed = speed;
        }
        animator = GetComponent<Animator>();
    }

    void OnPlayerDamageSense(Player player)
    {
        if (canDamage)
        {
            canDamage = false;
            player.Harm(damageDealt);
        }
    }

    void OnPlayerDamageUnsense(Player player)
    {
        if (!tickGoing)
        {
            StartCoroutine(TickDelay());
        }
    }

    void Update()
    {
        if (proximitySensor.IsClose())
        {
            animator.SetFloat("Move X", Player.instance.transform.position.x - transform.position.x);
            animator.SetFloat("Move Y", Player.instance.transform.position.y - transform.position.y);
            if (useNavMesh)
            {
                agent.SetDestination(Player.instance.transform.position);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.instance.transform.position, speed * Time.deltaTime);
            }
        }
    }

    protected override void Drop()
    {
        Player.instance.GainDrops(xpGiven, goldDropped);
    }

    private IEnumerator TickDelay()
    {
        tickGoing = true;
        Debug.Log("Tick");
        yield return new WaitForSeconds(1f);
        canDamage = true;
        tickGoing = false;
    }
}
