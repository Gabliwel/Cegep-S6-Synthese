using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMountainSpawnsManager : MonoBehaviour
{
    [Header("Rock")]
    [SerializeField] private int rockNb = 5;
    [SerializeField] private BossMountainRock rockPrefab;
    [Header("Stalagmite")]
    [SerializeField] private int stalagmiteNb = 10;
    [SerializeField] private BossMountainStalagmite stalagmitePrefab;
    [Header("Enemy")]
    [SerializeField] private int enemyNb = 20;
    [SerializeField] private BossMountainEnemy enemyPrefab;
    private BossMountainRock[] rocks;
    private BossMountainStalagmite[] stalagmites;
    private BossMountainEnemy[] enemies;

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

    public BossMountainRock GetAvailableRock()
    {
        foreach (BossMountainRock rock in rocks)
        {
            if (!rock.gameObject.activeSelf)
                return rock;
        }

        rocks[rocks.Length - 1].gameObject.SetActive(false);
        return rocks[rocks.Length - 1];
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
}
