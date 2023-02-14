using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;

public class RandomGenerationGameObject : MonoBehaviour
{
    [SerializeField] GameObject chest;
    [SerializeField] List<Vector3Int> posibleSpawn;
    [SerializeField] int mapMinChest;
    [SerializeField] bool redoNavMesh;
    private int chanceOfBonus = 90;
    private bool stopSpawn = false;

    void Start()
    {
        for (int i = 0; i < mapMinChest; i++)
        {
            RandomSpawn();
        }

        while (!stopSpawn)
        {
            if (posibleSpawn.Count > 0)
            {
                int spawnRolledNumber = Random.Range(0, 100);

                if (spawnRolledNumber < chanceOfBonus)
                {
                    RandomSpawn();
                    chanceOfBonus -= 15;
                    if (chanceOfBonus < 15)
                    {
                        chanceOfBonus = 15;
                    }
                }
                else
                {
                    stopSpawn = true;
                    Debug.Log("Stopped random");
                }
            }
            else
            {
                stopSpawn = true;
                Debug.Log("Stopped random");
            }
        }
        if (redoNavMesh)
        {
            NavMeshSurface nav = GameObject.FindGameObjectWithTag("Navmesh").GetComponent<NavMeshSurface>();
            nav.BuildNavMesh();
        }
    }

    private void RandomSpawn()
    {
        int chosen = Random.Range(0, posibleSpawn.Count - 1);

        Vector3Int spawnChosen = posibleSpawn[chosen];

        GameObject chestClone = Instantiate(chest);

        chestClone.transform.position = spawnChosen;

        chestClone.SetActive(true);

        posibleSpawn.Remove(spawnChosen);
    }
}
