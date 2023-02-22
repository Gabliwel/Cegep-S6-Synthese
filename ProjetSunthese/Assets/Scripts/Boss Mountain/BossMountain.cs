using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMountain : Enemy
{
    [Header("Rock")]
    [SerializeField] private BossMountainRock rockAttackPrefab;
    [SerializeField] private float rockThrowMinTime;
    [SerializeField] private float rockThrowMaxTime;
    [Header("Stalagmite")]
    [SerializeField] private BossMountainStalagmiteSpawner stalagmiteAttackPrefab;
    [SerializeField] private float stalagmiteSpawnMinTime;
    [SerializeField] private float stalagmiteSpawnMaxTime;
    [Header("Enemy")]
    [SerializeField] private BossMountainEnemySpawner enemyAttackPrefab;
    [SerializeField] private float enemySpawnMinTime;
    [SerializeField] private float enemySpawnMaxTime;
    [Header("Positions")]
    [SerializeField] private Vector3Int positionTop;
    [SerializeField] private Vector3Int positionBottom;
    [SerializeField] private Vector3Int positionRight;
    [SerializeField] private Vector3Int positionLeft;
    [SerializeField] private float positionChangeBaseTime;
    [Header("Timers")]
    [SerializeField] private float rockThrowTimer;
    [SerializeField] private float stalagmiteSpawnTimer;
    [SerializeField] private float enemySpawnTimer;
    [SerializeField] private float positionChangeTimer;

    private Vector3 rockThrowOffset = new Vector3(0, 3, 0);
    private Player player;
    private Animator animator;
    private Transform childLight;
    private BossMountainEnemySpawner enemySpawner;
    private BossMountainStalagmiteSpawner stalagmiteSpawner;
    private BossMountainRock rock;

    private HPBar hpBar;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        childLight = transform.Find("Light 2D");

        enemySpawner = Instantiate(enemyAttackPrefab);
        enemySpawner.transform.parent = transform;
        stalagmiteSpawner = Instantiate(stalagmiteAttackPrefab);
        stalagmiteSpawner.transform.parent = transform;
        rock = Instantiate(rockAttackPrefab);
        rock.transform.parent = transform;
        hpBar = GetComponentInChildren<HPBar>();
    }

    private void OnEnable()
    {
        enemySpawnTimer = Random.Range(enemySpawnMinTime, enemySpawnMaxTime);
        stalagmiteSpawnTimer = Random.Range(stalagmiteSpawnMinTime, stalagmiteSpawnMaxTime);
        rockThrowTimer = Random.Range(rockThrowMinTime, rockThrowMaxTime);
        positionChangeTimer = positionChangeBaseTime;
        hp = Scaling.instance.CalculateHealthOnScaling(baseHP);
        damageDealt = Scaling.instance.CalculateDamageOnScaling(baseDamageDealt);
    }

    protected override void Drop()
    {
        GetComponent<BossDrops>().BossDrop(transform.position);
    }

    public override void Die()
    {
        Scaling.instance.ScalingIncrease();
        base.Die();
    }

    public override void Harm(float ammount, float poison)
    {
        base.Harm(ammount, poison);
        hpBar.UpdateHp(hp, Scaling.instance.CalculateHealthOnScaling(baseHP));
    }

    protected override void WasPoisonHurt()
    {
        base.WasPoisonHurt();
        hpBar.UpdateHp(hp, Scaling.instance.CalculateHealthOnScaling(baseHP));
    }

    protected void Update()
    {
        //debug
        if (Input.GetKeyDown(KeyCode.F))
            StartRockThrow();
        if (Input.GetKeyDown(KeyCode.G))
            StartStalagmites();
        if (Input.GetKeyDown(KeyCode.H))
            StartEnemySpawn();

        if (enemySpawnTimer > 0)
            enemySpawnTimer -= Time.deltaTime;
        else
        {
            StartEnemySpawn();
            enemySpawnTimer = Random.Range(enemySpawnMinTime, enemySpawnMaxTime);
        }
        if (stalagmiteSpawnTimer > 0)
            stalagmiteSpawnTimer -= Time.deltaTime;
        else
        {
            StartStalagmites();
            stalagmiteSpawnTimer = Random.Range(stalagmiteSpawnMinTime, stalagmiteSpawnMaxTime);
        }
        if (rockThrowTimer > 0)
            rockThrowTimer -= Time.deltaTime;
        else
        {
            StartRockThrow();
            rockThrowTimer = Random.Range(rockThrowMinTime, rockThrowMaxTime);
        }
        if (positionChangeTimer > 0)
            positionChangeTimer -= Time.deltaTime;
        else
        {
            StartPositionChange();
            positionChangeTimer = positionChangeBaseTime;
        }
    }

    void StartRockThrow()
    {
        rock.Launch();
        rock.transform.position = transform.position + rockThrowOffset;
    }

    void StartStalagmites()
    {
        stalagmiteSpawner.Launch();
    }

    void StartEnemySpawn()
    {
        enemySpawner.Launch();


    }

    void StartPositionChange()
    {
        int position = Random.Range(0, 4);

        switch (position)
        {
            case 0: // top
                transform.position = positionTop;
                childLight.rotation = Quaternion.Euler(0, 0, 0);
                animator.SetTrigger("Top");
                break;
            case 1: // bottom
                transform.position = positionBottom;
                childLight.rotation = Quaternion.Euler(0, 0, 180);
                animator.SetTrigger("Bottom");
                break;
            case 2: // left
                transform.position = positionLeft;
                childLight.rotation = Quaternion.Euler(0, 0, 90);
                animator.SetTrigger("Left");
                break;
            case 3: // right
                transform.position = positionRight;
                childLight.rotation = Quaternion.Euler(0, 0, -90);
                animator.SetTrigger("Right");
                break;
        }
    }


}
