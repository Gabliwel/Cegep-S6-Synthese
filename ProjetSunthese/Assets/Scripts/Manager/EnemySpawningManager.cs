using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyChoice;
    [SerializeField] private SpawnerController[] allSpawners;
    [SerializeField] private List<SpawnerController> availableSpawners;
    [SerializeField] private int enemyListSize;
    [SerializeField] private GameObject[] spawnableEnemies;
    void Awake()
    {
        spawnableEnemies = new GameObject[enemyListSize];
        for (int i = 0; i < enemyListSize; i++)
        {
            spawnableEnemies[i] = Instantiate(enemyChoice[Random.Range(0,enemyChoice.Length)]);
            spawnableEnemies[i].SetActive(false);
        }
    }
    private void Start()
    {
        StartCoroutine(Spawn());
    }

    void Update()
    {
        
    }

    private IEnumerator Spawn()
    {
        SpawnerController currentSpawner;
        while (true)
        {
            foreach(GameObject enemy in spawnableEnemies)
            {
                if (!enemy.activeSelf)
                {
                    MakeListOfAvailableSpawner();
                    currentSpawner = availableSpawners[Random.Range(0, availableSpawners.Count)];
                    enemy.transform.position = currentSpawner.transform.position;
                    enemy.SetActive(true);
                    break;
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

    bool CheckIfSpawnerIsAvailable(SpawnerController spawner)
    {
        return spawner.isOccupied;
    }

    void MakeListOfAvailableSpawner()
    {
        availableSpawners = new List<SpawnerController>();
        foreach( SpawnerController spawner in allSpawners)
        {
            if (!CheckIfSpawnerIsAvailable(spawner))
            {
                availableSpawners.Add(spawner);
            }
        }
    }

}
