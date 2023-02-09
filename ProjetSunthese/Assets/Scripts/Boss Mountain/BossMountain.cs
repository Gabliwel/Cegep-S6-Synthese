using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMountain : Enemy
{
    [Header("Rock")]
    [SerializeField] private float rockThrowMinTime;
    [SerializeField] private float rockThrowMaxTime;
    [Header("Stalagmite")]
    [SerializeField] private int minStalagmiteSpawnNb;
    [SerializeField] private int maxStalagmiteSpawnNb;
    [SerializeField] private float stalagmiteSpawnMinTime;
    [SerializeField] private float stalagmiteSpawnMaxTime;
    [Header("Enemy")]
    [SerializeField] private int minEnemySpawnNb;
    [SerializeField] private int maxEnemySpawnNb;
    [SerializeField] private float enemySpawnMinTime;
    [SerializeField] private float enemySpawnMaxTime;
    [Header("Positions")]
    [SerializeField] private Vector2Int minSpawnRange;
    [SerializeField] private Vector2Int maxSpawnRange;
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
    private BossMountainSpawnsManager spawnsManager;
    private Animator animator;
    private Transform childLight;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spawnsManager = GetComponent<BossMountainSpawnsManager>();
        animator = GetComponent<Animator>();
        childLight = transform.Find("Light 2D");
    }

    private void OnEnable()
    {
        enemySpawnTimer = Random.Range(enemySpawnMinTime, enemySpawnMaxTime);
        stalagmiteSpawnTimer = Random.Range(stalagmiteSpawnMinTime, stalagmiteSpawnMaxTime);
        rockThrowTimer = Random.Range(rockThrowMinTime, rockThrowMaxTime);
        positionChangeTimer = positionChangeBaseTime;
    }

    protected override void Drop()
    {
    }

    private void Update()
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
        BossMountainRock rock = spawnsManager.GetAvailableRock();
        rock.gameObject.SetActive(true);
        rock.transform.position = transform.position + rockThrowOffset;
    }

    void StartStalagmites()
    {
        BossMountainStalagmite trackingStalagmite = spawnsManager.GetAvailableStalagamite();
        trackingStalagmite.transform.position = player.transform.position;
        trackingStalagmite.gameObject.SetActive(true);

        int ammount = Random.Range(minStalagmiteSpawnNb, maxStalagmiteSpawnNb);

        for (int i = 0; i < ammount; i++)
        {
            BossMountainStalagmite stalagmite = spawnsManager.GetAvailableStalagamite();
            stalagmite.transform.position = GetRandomSpawnLocation();
            stalagmite.gameObject.SetActive(true);
        }
    }

    void StartEnemySpawn()
    {
        int ammount = Random.Range(minEnemySpawnNb, maxEnemySpawnNb);

        for (int i = 0; i < ammount; i++)
        {
            BossMountainEnemy enemy = spawnsManager.GetAvailableEnemy();
            enemy.transform.position = GetRandomSpawnLocation();
            enemy.gameObject.SetActive(true);
        }
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

    Vector3Int GetRandomSpawnLocation()
    {
        int x = Random.Range(minSpawnRange.x, maxSpawnRange.x);
        int y = Random.Range(minSpawnRange.y, maxSpawnRange.y);

        return new Vector3Int(x, y, 0);
    }
}
