using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HouseRandom : MonoBehaviour
{
    [SerializeField] Tile[] house;
    [SerializeField] Tile[] ruins;
    [SerializeField] List<Vector3Int> posibleSpawn;
    [SerializeField] BoundsInt area;
    void Start()
    {
        Tilemap tilemap = GetComponent<Tilemap>();

        for (int i = 0; i < posibleSpawn.Count + 1; i++)
        {
            int spawnRolledNumber = Random.Range(0, 2);

            if (spawnRolledNumber == 1)
            {
                int chosen = Random.Range(0, posibleSpawn.Count - 1);
                Vector3Int spawnChosen = posibleSpawn[chosen];

                int tileNb = 0;

                for(int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        tilemap.SetTile(new Vector3Int(spawnChosen.x + x, spawnChosen.y - y, 0), house[tileNb]);
                        tileNb++;
                    }
                }
                posibleSpawn.Remove(spawnChosen);
            }
            if (spawnRolledNumber == 0)
            {
                int chosen = Random.Range(0, posibleSpawn.Count - 1);
                Vector3Int spawnChosen = posibleSpawn[chosen];

                int tileNb = 0;

                for (int y = 0; y < 4; y++)
                {
                    for (int x = 0; x < 4; x++)
                    {
                        tilemap.SetTile(new Vector3Int(spawnChosen.x + x, spawnChosen.y - y, 0), ruins[tileNb]);
                        tileNb++;
                    }
                }
                posibleSpawn.Remove(spawnChosen);
            }
        }
    }

    void Update()
    {
        
    }
}
