using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : Enemy
{
    [Header("Link")]
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject shield;
    [SerializeField] private Transform[] positions;
    private int currentPositionIndex = 0; // Check Serialize first position (0)

    private GrowingAttackZone growingZoneAttack;
    private LaserCircleAttack rangedCircleAttack;
    private LavaThrowAttack lavaThrowAttack;
    private Animator bossAnimator;
    private bool isProtected = false;

    [Header("Other stats")]
    [SerializeField] private float maxHp;
    private float hpToWatch;
    [SerializeField] private float shieldLeft = 3;
    [SerializeField] private float shieldLifeFraction = 0.25f;
    [SerializeField] private float bossSpeed = 3;

    void Start()
    {
        maxHp = hp;
        hpToWatch = maxHp * (shieldLifeFraction * shieldLeft);

        growingZoneAttack = gameObject.GetComponentInChildren<GrowingAttackZone>();
        rangedCircleAttack = gameObject.GetComponentInChildren<LaserCircleAttack>();
        lavaThrowAttack = gameObject.GetComponentInChildren<LavaThrowAttack>();
        bossAnimator = boss.GetComponent<Animator>();
        shield.SetActive(false);
    }

    void Update()
    {
        if (isProtected) return;

        if(hp <= hpToWatch && hp > 0)
        {
            StartCoroutine(ShieldTime());
        }


        // Pour tests
        if(Input.GetKeyDown(KeyCode.O) && growingZoneAttack.IsAvailable())
        {
            growingZoneAttack.Launch();
        }

        if(Input.GetKeyDown(KeyCode.P) && rangedCircleAttack.IsAvailable())
        {
            rangedCircleAttack.Launch();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            lavaThrowAttack.Launch();
        }
    }

    private IEnumerator ShieldTime()
    {
        isProtected = true;
        shield.SetActive(true);

        shieldLeft--;
        hpToWatch = maxHp * (shieldLifeFraction * shieldLeft);

        int positionIndex = GetPositionIndex();

        while (!growingZoneAttack.IsAvailable() || !rangedCircleAttack.IsAvailable()) { yield return null; }

        //For first frame
        bossAnimator.SetFloat("Move X", positions[positionIndex].position.x - transform.position.x);
        bossAnimator.SetFloat("Move Y", positions[positionIndex].position.y - transform.position.y);
        bossAnimator.SetBool("Move", true);

        yield return new WaitForSeconds(0.2f);
        while(transform.position != positions[positionIndex].position)
        {
            bossAnimator.SetFloat("Move X", positions[positionIndex].position.x - transform.position.x);
            bossAnimator.SetFloat("Move Y", positions[positionIndex].position.y - transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, positions[positionIndex].position, 2 * Time.deltaTime);
            yield return null;
        }
        bossAnimator.SetBool("Move", false);
        yield return new WaitForSeconds(1f);
        shield.SetActive(false);
        isProtected = false;
    }

    private int GetPositionIndex()
    {
        int randomNr = 0;

        while (randomNr == currentPositionIndex)
        {
            randomNr = UnityEngine.Random.Range(0, positions.Length);
        }
        currentPositionIndex = randomNr;
        return randomNr;
    }

    public override void Harm(float ammount)
    {
        if (isProtected) return;
        base.Harm(ammount);
    }

    public override void Harm(float ammount, float overtimeDamage)
    {
        if (isProtected) return;
        base.Harm(ammount, overtimeDamage);
    }

    protected override void Drop()
    {
        
    }
}
