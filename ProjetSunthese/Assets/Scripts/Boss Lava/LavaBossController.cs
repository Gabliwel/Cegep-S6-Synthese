using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaBossController : Enemy
{
    [SerializeField] private GameObject trail;
    [SerializeField]private int trailListSize = 50;
    [SerializeField] GameObject lavaAuraChild;
    private GameObject[] trailArray;
    float trailTimeElapsed = 0;
    float attackTimer = 0;
    LavaAura lavaAura;
    LavaShockWaveController lavaShockWave;



    void Awake()
    {
        lavaShockWave = GetComponentInChildren<LavaShockWaveController>(true);
        lavaAura = GetComponentInChildren<LavaAura>(true);
        trailArray = new GameObject[trailListSize];
        for (int i = 0; i < trailListSize; i++)
        {
            trailArray[i] = Instantiate(trail);
            trailArray[i].SetActive(false);
        }
    }

    void Update()
    {
        trailTimeElapsed += Time.deltaTime;
        attackTimer += Time.deltaTime;
        if(attackTimer > 15)
        {
            lavaAura.Launch();
            lavaShockWave.Launch();
            attackTimer = 0;
        }
        gameObject.transform.position = gameObject.transform.position + new Vector3(0.001f, 0.001f, 0);
        LeaveTrail();
    }

    public void LeaveTrail()
    {
        if (trailTimeElapsed >= 3)
        {
            for (int i = 0; i < trailListSize; i++)
            {
                if (!trailArray[i].activeSelf)
                {
                    trailArray[i].transform.position = transform.position;
                    trailArray[i].SetActive(true);
                    break;
                }
            }
            trailTimeElapsed = 0;
        }
    }

 
    protected override void Drop()
    {
        Debug.Log("Je drop");
    }
}
