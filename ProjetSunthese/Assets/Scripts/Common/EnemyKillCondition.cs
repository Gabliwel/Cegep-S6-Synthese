using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillCondition : MonoBehaviour
{
    [SerializeField] private GameObject rewardPrefab;
    [SerializeField] private Vector3 rewardLocation;
    private GameObject reward;
    private Enemy[] enemies;
    bool rewardSpawned = false;

    private void Awake()
    {
        enemies = GetComponentsInChildren<Enemy>();
        reward = Instantiate(rewardPrefab);
        reward.transform.parent = transform;
        reward.SetActive(false);
    }


    private void LateUpdate()
    {
        if (rewardSpawned) return;
        bool spawnReward = true;
        foreach (Enemy enemy in enemies)
        {
            if (enemy.gameObject.activeInHierarchy)
            {
                spawnReward = false;
            }
        }
        if (spawnReward)
            SpawnReward();
    }

    private void SpawnReward()
    {
        if (rewardSpawned) return;
        rewardSpawned = true;
        reward.SetActive(true);
        reward.transform.position = rewardLocation;
    }
}
