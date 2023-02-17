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
    private LavaAura lavaAura;
    private LavaShockWaveController lavaShockWave;
    private GameObject player;
    private int speed =3 ;
    private Animator animator;
    private HPBar hpBar;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        lavaShockWave = GetComponentInChildren<LavaShockWaveController>(true);
        lavaAura = GetComponentInChildren<LavaAura>(true);
        trailArray = new GameObject[trailListSize];
        for (int i = 0; i < trailListSize; i++)
        {
            trailArray[i] = Instantiate(trail);
            trailArray[i].SetActive(false);
        }
        player =GameObject.FindGameObjectWithTag("Player");
        hpBar = GetComponentInChildren<HPBar>();
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
        animator.SetBool("Move", true);
        animator.SetFloat("Move X", player.transform.position.x - transform.position.x);
        animator.SetFloat("Move Y", player.transform.position.y - transform.position.y);
        gameObject.transform.position = Vector2.MoveTowards(transform.position,player.transform.position, speed * Time.deltaTime);
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
        GetComponent<BossDrops>().BossDrop(transform.position);
    }

    public override void Harm(float ammount, float poison)
    {
        Scaling.instance.ScalingIncrease();
        base.Harm(ammount, poison);
        hpBar.UpdateHp(hp, Scaling.instance.CalculateHealthOnScaling(baseHP));
    }
}
