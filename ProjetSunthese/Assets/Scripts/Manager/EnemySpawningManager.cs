using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyChoice;
    [SerializeField] private SpawnerController[] allSpawners;
    [SerializeField] private List<SpawnerController> availableSpawners;
    [SerializeField] private int enemyListSize;
    [SerializeField] private Enemy[] spawnableEnemies;
    [SerializeField] private int spawningInterval = 5;

    [SerializeField] private int buffer = 0;

    private bool canSpawn = true;

    void Awake()
    {
        spawnableEnemies = new Enemy[enemyListSize];
        for (int i = 0; i < enemyListSize; i++)
        {
            spawnableEnemies[i] = Instantiate(enemyChoice[Random.Range(0,enemyChoice.Length)]).GetComponent<Enemy>();
            spawnableEnemies[i].gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private void OnEnable()
    {
        canSpawn = true;
    }

    private void OnDisable()
    {
        canSpawn = false;
        foreach (Enemy enemy in spawnableEnemies) if(enemy != null) enemy.gameObject.SetActive(false);
    }

    private IEnumerator Spawn()
    {
        SpawnerController currentSpawner;
        while (true)
        {
            if (!canSpawn) yield return null;
            foreach (Enemy enemy in spawnableEnemies)
            {
                if (!enemy.gameObject.activeSelf)
                {
                    MakeListOfAvailableSpawner();
                    if(availableSpawners.Count > 0 + buffer)
                    {
                        currentSpawner = availableSpawners[Random.Range(0, availableSpawners.Count - buffer)];
                        enemy.transform.position = currentSpawner.transform.position;
                        enemy.gameObject.SetActive(true);
                        enemy.ChangeLayer(currentSpawner.gameObject.layer);
                    }
                    break;
                }
            }
            yield return new WaitForSeconds(spawningInterval);
        }
    }

    bool CheckIfSpawnerIsAvailable(SpawnerController spawner)
    {
        return spawner.IsOccupied();
    }

    void MakeListOfAvailableSpawner()
    {
        availableSpawners.Clear();
        foreach( SpawnerController spawner in allSpawners)
        {
            if (!CheckIfSpawnerIsAvailable(spawner))
            {
                availableSpawners.Add(spawner);
            }
        }
    }

}
