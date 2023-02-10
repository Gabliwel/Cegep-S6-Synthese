using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrerShieldMinionSpawner : BossAttack
{
    [SerializeField] private int minEnemySpawnNb;
    [SerializeField] private int maxEnemySpawnNb;
    [SerializeField] private int enemyNb = 20;
    [SerializeField] private BossBofrerShieldMinion enemyPrefab;
    private BossBofrerShieldMinion[] enemies;
    public override void Launch()
    {
        int ammount = Random.Range(minEnemySpawnNb, maxEnemySpawnNb);

        for (int i = 0; i < ammount; i++)
        {
            BossBofrerShieldMinion enemy = GetAvailableEnemy();
            enemy.gameObject.SetActive(true);
            enemy.SetRotation(GetRandomRotation());
        }
    }

    private void Awake()
    {
        enemies = new BossBofrerShieldMinion[enemyNb];
        for (int i = 0; i < enemyNb; i++)
        {
            enemies[i] = GameObject.Instantiate<BossBofrerShieldMinion>(enemyPrefab);
            enemies[i].gameObject.SetActive(false);
            enemies[i].transform.position = transform.position;
        }
    }

    private float GetRandomRotation()
    {
        return Random.Range(0, 360f);
    }

    public BossBofrerShieldMinion GetAvailableEnemy()
    {
        foreach (BossBofrerShieldMinion enemy in enemies)
        {
            if (!enemy.gameObject.activeSelf)
                return enemy;
        }

        enemies[enemies.Length - 1].gameObject.SetActive(false);
        return enemies[enemies.Length - 1];
    }

    public bool AnyMinionActive()
    {
        foreach (BossBofrerShieldMinion enemy in enemies)
        {
            if (enemy.gameObject.activeSelf)
                return true;
        }
        return false;
    }

}
