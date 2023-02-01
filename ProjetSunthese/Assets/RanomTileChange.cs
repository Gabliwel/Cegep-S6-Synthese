using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.AI;
using NavMeshPlus.Components;

public class RanomTileChange : MonoBehaviour
{
    [SerializeField] Tile coffre;
    [SerializeField] GameObject chestCollider;
    [SerializeField] List<Vector3Int> posibleSpawn;
    [SerializeField] int mapMinChest;
    private int chanceOfBonus = 90;
    private bool stopSpawn = false;
    private Tilemap tilemap;
    void Start()
    {
        tilemap = GetComponent<Tilemap>();

        for(int i = 0; i < mapMinChest; i++)
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
                    if(chanceOfBonus < 15)
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
        NavMeshSurface nav = GameObject.FindGameObjectWithTag("Navmesh").GetComponent<NavMeshSurface>();
        nav.BuildNavMesh();
    }

    private void RandomSpawn()
    {
        int chosen = Random.Range(0, posibleSpawn.Count - 1);
        Vector3Int spawnChosen = posibleSpawn[chosen];
        tilemap.SetTile(new Vector3Int(spawnChosen.x, spawnChosen.y, 0), coffre);
        GameObject currentChestHitbox = Instantiate(chestCollider);
        Vector3 worldPos = tilemap.CellToWorld(new Vector3Int(spawnChosen.x, spawnChosen.y, 0));
        currentChestHitbox.transform.position = new Vector3(worldPos.x + 0.5f, worldPos.y + 0.5f, 0);
        posibleSpawn.Remove(spawnChosen);
    }

    void Update()
    {
       
    }
}
