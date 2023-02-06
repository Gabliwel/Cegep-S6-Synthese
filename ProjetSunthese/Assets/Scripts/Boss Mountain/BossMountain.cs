using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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

    private BossMountainRock[] rocks;
    private BossMountainStalagmite[] stalagmites;
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
    }

    void StartRockThrow()
    {
        BossMountainRock rock = GetAvailableRock();
        rock.gameObject.SetActive(true);
        rock.SetDestination(player.transform.position);
        rock.transform.position = transform.position + rockThrowOffset;
    }

    void StartStalagmites()
    {
        BossMountainStalagmite trackingStalagmite = GetAvailableStalagamite();
        trackingStalagmite.gameObject.SetActive(false);
        Vector3 trackingLocation = player.transform.position;
        trackingStalagmite.transform.position = trackingLocation;
        trackingStalagmite.gameObject.SetActive(true);

        int ammount = Random.Range(minStalagmiteSpawnNb, maxStalagmiteSpawnNb);

        for(int i = 0; i < ammount; i++)
        {
            BossMountainStalagmite stalagmite = GetAvailableStalagamite();
            stalagmite.gameObject.SetActive(false);
            Vector3 stalagmiteSpawn = GetRandomPillarSpawn();
            stalagmite.transform.position = stalagmiteSpawn;
            stalagmite.gameObject.SetActive(true);
        }
    }

    Vector3Int GetRandomPillarSpawn()
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
}
