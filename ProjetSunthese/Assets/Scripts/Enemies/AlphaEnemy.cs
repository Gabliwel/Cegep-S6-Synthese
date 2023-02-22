using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlphaEnemy : Enemy
{
    [SerializeField] private float speed = 5;

    [SerializeField] bool useNavMesh;

    private Sensor damageSensor;
    private Sensor rangeSensor;
    private ISensor<Player> playerDamageSensor;
    private ISensor<Player> playerRangeSensor;
    private bool canDamage = true;
    private bool tickGoing = false;

    private Player player;

    private NavMeshAgent agent;
    private Animator animator;

    void Awake()
    {
        rangeSensor = transform.Find("RangeSensor").GetComponent<Sensor>();
        damageSensor = transform.Find("DamageSensor").GetComponent<Sensor>();

        playerRangeSensor = rangeSensor.For<Player>();
        playerDamageSensor = damageSensor.For<Player>();

        playerRangeSensor.OnSensedObject += OnPlayerRangeSense;
        playerRangeSensor.OnUnsensedObject += OnPlayerRangeUnsense;

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

    void OnPlayerRangeSense(Player player)
    {
        this.player = player;
    }

    void OnPlayerRangeUnsense(Player player)
    {
        this.player = null;
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
        if (player != null)
        {
            animator.SetFloat("Move X", player.transform.position.x - transform.position.x);
            animator.SetFloat("Move Y", player.transform.position.y - transform.position.y);
            if (useNavMesh)
            {
                agent.SetDestination(player.transform.position);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
        }
    }

    protected override void Drop()
    {
        Player.instance.GainDrops(0, xpGiven, goldDropped);
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
