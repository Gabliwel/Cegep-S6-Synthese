using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMountain : Enemy
{
    [SerializeField] private BossMountainRock rockPrefab;
    private BossMountainRock rock;
    private Vector3 rockThrowOffset = new Vector3(0, 3, 0);
    private Player player;

    private void Awake()
    {
        rock = GameObject.Instantiate<BossMountainRock>(rockPrefab);
        rock.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    protected override void Drop()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            StartRockThrow();
    }

    void StartRockThrow()
    {
        rock.gameObject.SetActive(true);
        rock.SetDestination(player.transform.position);
        rock.transform.position = transform.position + rockThrowOffset;
    }
}
