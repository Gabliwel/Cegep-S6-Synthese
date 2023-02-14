using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMountainEnemySpawner : BossAttack
{
    [SerializeField] private int minEnemySpawnNb;
    [SerializeField] private int maxEnemySpawnNb;
    [SerializeField] private int enemyNb = 20;
    [SerializeField] private BossMountainEnemy enemyPrefab;
    private Vector3 minSpawnRange;
    private Vector3 maxSpawnRange;
    private BossMountainEnemy[] enemies;

    private void Awake()
    {
        enemies = new BossMountainEnemy[enemyNb];
        for (int i = 0; i < enemyNb; i++)
        {
            enemies[i] = GameObject.Instantiate<BossMountainEnemy>(enemyPrefab);
            enemies[i].gameObject.SetActive(false);
        }

        minSpawnRange = GameObject.FindGameObjectWithTag("ArenaLimitTopLeft").transform.position;
        maxSpawnRange = GameObject.FindGameObjectWithTag("ArenaLimitBottomRight").transform.position;
        type = BossAttackType.Mountain;
    }

    public override void Launch()
    {
        int ammount = Random.Range(minEnemySpawnNb, maxEnemySpawnNb);

        for (int i = 0; i < ammount; i++)
        {
            BossMountainEnemy enemy = GetAvailableEnemy();
            enemy.transform.position = GetRandomSpawnLocation();
            enemy.gameObject.SetActive(true);
        }
    }

    public BossMountainEnemy GetAvailableEnemy()
    {
        foreach (BossMountainEnemy enemy in enemies)
        {
            if (!enemy.gameObject.activeSelf)
                return enemy;
        }

        enemies[enemies.Length - 1].gameObject.SetActive(false);
        return enemies[enemies.Length - 1];
    }

    Vector3 GetRandomSpawnLocation()
    {
        float x = Random.Range(minSpawnRange.x, maxSpawnRange.x);
        float y = Random.Range(minSpawnRange.y, maxSpawnRange.y);

        return new Vector3(x, y, 0);
    }
}
