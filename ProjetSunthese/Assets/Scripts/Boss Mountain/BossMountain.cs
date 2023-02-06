using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BossMountain : Enemy
{
    [SerializeField] private BossMountainRock rockPrefab;
    [SerializeField] private BossMountainStalagmite stalagmitePrefab;
    [SerializeField] private Vector2Int minPillarSpawn;
    [SerializeField] private Vector2Int maxPillarSpawn;
    private BossMountainRock rock;
    private BossMountainStalagmite stalagmite;
    private Vector3 rockThrowOffset = new Vector3(0, 3, 0);
    private Player player;


    private void Awake()
    {
        rock = GameObject.Instantiate<BossMountainRock>(rockPrefab);
        rock.gameObject.SetActive(false);
        stalagmite = GameObject.Instantiate<BossMountainStalagmite>(stalagmitePrefab);
        stalagmite.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    protected override void Drop()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            StartRockThrow();
        if (Input.GetKeyDown(KeyCode.G))
            StartStalagmites();
    }

    void StartRockThrow()
    {
        rock.gameObject.SetActive(true);
        rock.SetDestination(player.transform.position);
        rock.transform.position = transform.position + rockThrowOffset;
    }

    void StartStalagmites()
    {
        stalagmite.gameObject.SetActive(false);
        Vector3 stalagmiteSpawn = player.transform.position;//GetRandomPillarSpawn();

        stalagmite.transform.position = stalagmiteSpawn;
        stalagmite.gameObject.SetActive(true);
    }

    Vector3Int GetRandomPillarSpawn()
    {
        int x = Random.Range(minPillarSpawn.x, maxPillarSpawn.x);
        int y = Random.Range(minPillarSpawn.y, maxPillarSpawn.y);

        return new Vector3Int(x, y,0);
    }
}
