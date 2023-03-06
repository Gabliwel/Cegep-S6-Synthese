using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShieldEnemy : Enemy
{
    [SerializeField] private float speed = 5;

    [SerializeField] bool useNavMesh;

    [SerializeField] float attackSpeed;

    [SerializeField] private float knockBackForce = 10;

    private Sensor damageSensor;
    private ISensor<Player> playerDamageSensor;

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

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(AttackPlayerInRange());
    }

    private void Start()
    {
        startingHp = hp;
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

    public override void Harm(float ammount, float poison)
    {
        if (!shieldOn)
        {
            base.Harm(ammount, poison);
        }
        else
        {
            SoundMaker.instance.BofrerShieldHitSound(gameObject.transform.position);
        }

        if (hp <= (startingHp / 2) && !didShield)
        {
            didShield = true;
            shieldOn = true;
            shield.SetActive(true);
            if (hp > 0)
                StartCoroutine(ShieldTime());

        }
    }

    private IEnumerator ShieldTime()
    {
        yield return new WaitForSeconds(3f);
        shield.SetActive(false);
        shieldOn = false;
    }

    private IEnumerator AttackPlayerInRange()
    {
        while (isActiveAndEnabled)
        {
            if (TouchingPlayer())
            {
                Player.instance.Harm(damageDealt);
                Player.instance.KnockBack((Player.instance.gameObject.transform.position - transform.position).normalized, knockBackForce);
                yield return new WaitForSeconds(attackSpeed);
            }
            yield return null;
        }
    }

    private bool TouchingPlayer()
    {
        return playerDamageSensor.SensedObjects.Count > 0;
    }
}
