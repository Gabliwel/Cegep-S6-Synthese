using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMountainStalagmiteSpawner : BossAttack
{
    [SerializeField] private int stalagmiteNb = 10;
    [SerializeField] private BossMountainStalagmite stalagmitePrefab;
    [SerializeField] private int minStalagmiteSpawnNb;
    [SerializeField] private int maxStalagmiteSpawnNb;
    private Vector3 minSpawnRange;
    private Vector3 maxSpawnRange;
    private BossMountainStalagmite[] stalagmites;
    private Player player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        stalagmites = new BossMountainStalagmite[stalagmiteNb];
        for (int i = 0; i < stalagmiteNb; i++)
        {
            stalagmites[i] = GameObject.Instantiate<BossMountainStalagmite>(stalagmitePrefab);
            stalagmites[i].gameObject.SetActive(false);
        }
        minSpawnRange = GameObject.FindGameObjectWithTag("ArenaLimitTopLeft").transform.position;
        maxSpawnRange = GameObject.FindGameObjectWithTag("ArenaLimitBottomRight").transform.position;
        type = BossAttackType.Mountain;
    }
    public override void Launch()
    {
        BossMountainStalagmite trackingStalagmite = GetAvailableStalagamite();
        trackingStalagmite.transform.position = player.transform.position;
        trackingStalagmite.gameObject.SetActive(true);

        int ammount = Random.Range(minStalagmiteSpawnNb, maxStalagmiteSpawnNb) * Scaling.instance.SendScaling();

        for (int i = 0; i < ammount; i++)
        {
            BossMountainStalagmite stalagmite = GetAvailableStalagamite();
            stalagmite.transform.position = GetRandomSpawnLocation();
            stalagmite.gameObject.SetActive(true);
        }
        SoundMaker.instance.JgSpawnStalagmitesSound(transform.position);
    }

    public BossMountainStalagmite GetAvailableStalagamite()
    {
        foreach (BossMountainStalagmite stalagmite in stalagmites)
        {
            if (!stalagmite.gameObject.activeSelf)
                return stalagmite;
        }

        stalagmites[stalagmites.Length - 1].gameObject.SetActive(false);
        return stalagmites[stalagmites.Length - 1];
    }
    Vector3 GetRandomSpawnLocation()
    {
        float x = Random.Range(minSpawnRange.x, maxSpawnRange.x);
        float y = Random.Range(minSpawnRange.y, maxSpawnRange.y);

        return new Vector3(x, y, 0);
    }
}
