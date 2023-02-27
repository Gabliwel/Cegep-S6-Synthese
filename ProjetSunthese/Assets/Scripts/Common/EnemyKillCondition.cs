using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKillCondition : MonoBehaviour
{
    [SerializeField] private GenericItem rewardPrefab;
    [SerializeField] private Vector3 rewardLocation;
    private GenericItem reward;
    private Enemy[] enemies;
    bool rewardSpawned = false;

    private void Awake()
    {
        enemies = GetComponentsInChildren<Enemy>();
        reward = Instantiate(rewardPrefab);
        reward.transform.parent = transform;
        reward.SetItem(LayerMask.LayerToName(gameObject.layer), Billy.Rarity.ItemRarity.LEGENDARY);
        reward.gameObject.SetActive(false);
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
        reward.gameObject.SetActive(true);
        reward.transform.position = rewardLocation;
    }
}
