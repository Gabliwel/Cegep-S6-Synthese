using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShieldEnemy : Enemy
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
    private GameObject shield;

    private bool shieldOn = false;
    private bool didShield = false;
    private float startingHp = 0;

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

        shield = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        startingHp = hp;
    }

    void OnPlayerDamageSense(Player player)
    {
        if (canDamage)
        {
            canDamage = false;
            player.Harm(damageDealt);
        }
        Debug.Log("Yes");
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
        Player.instance.GainDrops(0, xpGiven, goldDropped);
    }

    public override void Harm(float ammount, float poison)
    {
        if (!shieldOn)
        {
            base.Harm(ammount, poison);
        }

        if (hp <= (startingHp / 2) && !didShield)
        {
            didShield = true;
            shieldOn = true;
            shield.SetActive(true);
            StartCoroutine(ShieldTime());
        }
    }

    private IEnumerator TickDelay()
    {
        tickGoing = true;
        Debug.Log("Tick");
        yield return new WaitForSeconds(1f);
        canDamage = true;
        tickGoing = false;
    }

    private IEnumerator ShieldTime()
    {
        Debug.Log("Shield");
        yield return new WaitForSeconds(3f);
        shield.SetActive(false);
        shieldOn = false;
    }
}
