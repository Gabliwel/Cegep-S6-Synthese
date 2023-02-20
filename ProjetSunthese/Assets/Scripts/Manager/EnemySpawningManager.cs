using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawningManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyChoice;
    [SerializeField] private GameObject[] spawners;
    [SerializeField] private int enemyListSize;
    [SerializeField] private int spawnerListSize;
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
        while (true)
        {
            foreach(GameObject enemy in spawnableEnemies)
            {
                if (!enemy.activeSelf)
                {
                    enemy.transform.position = spawners[Random.Range(0, spawnerListSize)].transform.position;
                    enemy.SetActive(true);
                   yield return new WaitForSeconds(10);
                }
            }
        }
    }
}
