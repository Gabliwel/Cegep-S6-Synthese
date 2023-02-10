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
    float auraTimeElapsed = 0;
    LavaAura lavaAura;
    LavaShockWaveController lavaShockWave;


    void Awake()
    {
        lavaShockWave = GetComponentInChildren<LavaShockWaveController>(true);
        lavaAura = GetComponentInChildren<LavaAura>(true);
        Debug.Log(lavaAura);
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
        auraTimeElapsed += Time.deltaTime;
        gameObject.transform.position = gameObject.transform.position + new Vector3(0.001f, 0.001f, 0);
        LeaveTrail();
        CalculateAuraTiming();
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

    public void CalculateAuraTiming()
    {
        if(auraTimeElapsed > 10)
        {
            lavaAuraChild.SetActive(true);
            lavaShockWave.gameObject.SetActive(true);
            gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }
        if(auraTimeElapsed > 20)
        {
            auraTimeElapsed = 0;
            lavaShockWave.gameObject.SetActive(false);
            gameObject.GetComponentInChildren<ParticleSystem>().Stop();
        }
    }

    [ContextMenu("NextLevel")]
    public void NextLevelTest()
    {
        Debug.Log("Hellooooooo");
        GameManager.instance.GetRandomNextLevelAndStart();
    }

    [ContextMenu("MainStage")]
    public void GetBackToMainStage()
    {
        GameManager.instance.GetBackToMainStageAndStart();
    }

    public override void Harm(float ammount)
    {
        base.Harm(ammount);
    }

    public override void Die()
    {
        base.Die();
    }

    protected override void Drop()
    {
        Debug.Log("Je drop");
    }
}
