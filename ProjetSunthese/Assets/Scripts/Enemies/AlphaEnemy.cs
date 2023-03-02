using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlphaEnemy : Enemy
{
    [SerializeField] private float speed = 5;

    [SerializeField] bool useNavMesh;

    [SerializeField] float attackSpeed;

    private Sensor damageSensor;
    private ISensor<Player> playerDamageSensor;

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

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(AttackPlayerInRange());
    }

    void OnPlayerDamageSense(Player player)
    {

    }

    void OnPlayerDamageUnsense(Player player)
    {

    }

    void Update()
    {
        if (proximitySensor.IsClose())
        {
            animator.SetFloat("Move X", Player.instance.transform.position.x - transform.position.x);
            animator.SetFloat("Move Y", Player.instance.transform.position.y - transform.position.y);
            if (!TouchingPlayer())
            {
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
    }

    protected override void Drop()
    {
        Player.instance.GainDrops(xpGiven, goldDropped);
    }

    private IEnumerator AttackPlayerInRange()
    {
        while (isActiveAndEnabled)
        {
            if (TouchingPlayer())
            {
                Player.instance.Harm(damageDealt);
                yield return new WaitForSeconds(attackSpeed);
            }
            yield return null;
        }
    }

    private bool TouchingPlayer()
    {
        Debug.Log(playerDamageSensor.SensedObjects.Count > 0);
        return playerDamageSensor.SensedObjects.Count > 0;
    }
}
