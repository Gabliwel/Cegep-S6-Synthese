using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMountain : Enemy
{
    [Header("Rock")]
    [SerializeField] private int rockNb = 5;
    [SerializeField] private BossMountainRock rockPrefab;
    [Header("Stalagmite")]
    [SerializeField] private int stalagmiteNb = 10;
    [SerializeField] private BossMountainStalagmite stalagmitePrefab;
    [SerializeField] private Vector2Int minStalagmiteSpawnRange;
    [SerializeField] private Vector2Int maxStalagmiteSpawnRange;
    [SerializeField] private int minStalagmiteSpawnNb;
    [SerializeField] private int maxStalagmiteSpawnNb;
    [Header("Enemy")]
    [SerializeField] private int enemyNb = 20;
    [SerializeField] private BossMountainEnemy enemyPrefab;
    [SerializeField] private Vector2Int minEnemySpawnRange;
    [SerializeField] private Vector2Int maxEnemySpawnRange;
    [SerializeField] private int minEnemySpawnNb;
    [SerializeField] private int maxEnemySpawnNb;

    private BossMountainRock[] rocks;
    private BossMountainStalagmite[] stalagmites;
    private BossMountainEnemy[] enemies;
    private Vector3 rockThrowOffset = new Vector3(0, 3, 0);
    private Player player;

    private void Awake()
    {
        rocks = new BossMountainRock[rockNb];
        for (int i = 0; i < rockNb; i++)
        {
            rocks[i] = GameObject.Instantiate<BossMountainRock>(rockPrefab);
            rocks[i].gameObject.SetActive(false);
        }

        stalagmites = new BossMountainStalagmite[stalagmiteNb];
        for (int i = 0; i < stalagmiteNb; i++)
        {
            stalagmites[i] = GameObject.Instantiate<BossMountainStalagmite>(stalagmitePrefab);
            stalagmites[i].gameObject.SetActive(false);
        }

        enemies = new BossMountainEnemy[enemyNb];
        for (int i = 0; i < enemyNb; i++)
        {
            enemies[i] = GameObject.Instantiate<BossMountainEnemy>(enemyPrefab);
            enemies[i].gameObject.SetActive(false);
        }

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    protected override void Drop()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            StartRockThrow();
        if (Input.GetKeyDown(KeyCode.G))
            StartStalagmites();
        if (Input.GetKeyDown(KeyCode.H))
            StartEnemySpawn();
    }

    void StartRockThrow()
    {
        BossMountainRock rock = GetAvailableRock();
        rock.gameObject.SetActive(true);
        rock.transform.position = transform.position + rockThrowOffset;
    }

    void StartStalagmites()
    {
        BossMountainStalagmite trackingStalagmite = GetAvailableStalagamite();
        trackingStalagmite.transform.position = player.transform.position;
        trackingStalagmite.gameObject.SetActive(true);

        int ammount = Random.Range(minStalagmiteSpawnNb, maxStalagmiteSpawnNb);

        for(int i = 0; i < ammount; i++)
        {
            BossMountainStalagmite stalagmite = GetAvailableStalagamite();
            stalagmite.transform.position = GetRandomSpawnLocation();
            stalagmite.gameObject.SetActive(true);
        }
    }

    void StartEnemySpawn()
    {
        int ammount = Random.Range(minEnemySpawnNb, maxEnemySpawnNb);

        for (int i = 0; i < ammount; i++)
        {
            BossMountainEnemy enemy = GetAvailableEnemy();
            enemy.transform.position = GetRandomSpawnLocation();
            enemy.gameObject.SetActive(true);
        }
    }

    Vector3Int GetRandomSpawnLocation()
    {
        int x = Random.Range(minStalagmiteSpawnRange.x, maxStalagmiteSpawnRange.x);
        int y = Random.Range(minStalagmiteSpawnRange.y, maxStalagmiteSpawnRange.y);

        return new Vector3Int(x, y, 0);
    }

    BossMountainStalagmite GetAvailableStalagamite()
    {
        foreach (BossMountainStalagmite stalagmite in stalagmites)
        {
            if (!stalagmite.gameObject.activeSelf)
                return stalagmite;
        }

        stalagmites[stalagmites.Length - 1].gameObject.SetActive(false);
        return stalagmites[stalagmites.Length - 1];
    }

    BossMountainRock GetAvailableRock()
    {
        foreach (BossMountainRock rock in rocks)
        {
            if (!rock.gameObject.activeSelf)
                return rock;
        }

        rocks[rocks.Length - 1].gameObject.SetActive(false);
        return rocks[rocks.Length - 1];
    }

    BossMountainEnemy GetAvailableEnemy()
    {
        foreach (BossMountainEnemy enemy in enemies)
        {
            if (!enemy.gameObject.activeSelf)
                return enemy;
        }

        enemies[enemies.Length - 1].gameObject.SetActive(false);
        return enemies[enemies.Length - 1];
    }
}
