using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExplosiveEnnemy : Enemy
{
    [SerializeField] private float speed = 3;
    [SerializeField] private GameObject explosion;
    [SerializeField] bool useNavMesh;

    private Sensor damageSensor;
    private Sensor rangeSensor;
    private ISensor<Player> playerDamageSensor;
    private ISensor<Player> playerRangeSensor;

    private bool exploding = false;

    private Player player;

    private NavMeshAgent agent;
    private Animator animator;

    void Awake()
    {
        rangeSensor = transform.Find("RangeSensor").GetComponent<Sensor>();
        damageSensor = transform.Find("DamageSensor").GetComponent<Sensor>();

        explosion = Instantiate(explosion);
        explosion.SetActive(false);

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

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void OnPlayerRangeSense(Player player)
    {
        if (!exploding)
        {
            exploding = true;
            StartCoroutine(ExplosionDelay());
        }
    }

    void OnPlayerRangeUnsense(Player player)
    {

    }

    void OnPlayerDamageSense(Player player)
    {
        player.Harm(damageDealt);
    }

    void OnPlayerDamageUnsense(Player player)
    {

    }

    void Update()
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

    protected override void Drop()
    {
        Player.instance.GainDrops(0, xpGiven, goldDropped);
    }

    private IEnumerator ExplosionDelay()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(1f);
        exploding = false;
        explosion.transform.position = gameObject.transform.position;
        explosion.SetActive(true);
        gameObject.SetActive(false);
    }
}
