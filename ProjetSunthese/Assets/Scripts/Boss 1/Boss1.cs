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
    private float growingZoneAttackEstimatedTime = 5;
    private LaserCircleAttack rangedCircleAttack;
    private LavaThrowAttack lavaThrowAttack;
    private Animator bossAnimator;
    private BossDrops drops;

    private bool isProtected = false;
    private bool isAttacking = false;

    [Header("Other stats")]
    private float hpToWatch;
    [SerializeField] private float shieldLeft = 3;
    [SerializeField] private float shieldLifeFraction = 0.25f;

    private HPBar HPbar;

    protected override void Awake()
    {
        base.Awake();
        HPbar = GetComponentInChildren<HPBar>();
    }

    void Start()
    {
        Debug.Log("maxHp: "+ baseHP);
        hp = baseHP;
        hpToWatch = baseHP * (shieldLifeFraction * shieldLeft);

        drops = GetComponent<BossDrops>();
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

        if (!isAttacking)
        {
            isAttacking = true;
            int rand = UnityEngine.Random.Range(1, 6);
            if(rand == 1)
            {
                StartCoroutine(Pattern1());
            }
            else if (rand == 2)
            {
                StartCoroutine(Pattern2());
            }
            else if (rand == 3)
            {
                StartCoroutine(Pattern3());
            }
            else if (rand == 4)
            {
                StartCoroutine(Pattern4());
            }
            else if (rand == 5)
            {
                StartCoroutine(Pattern5());
            }
        }
    }

    private IEnumerator Pattern1()
    {
        growingZoneAttack.Launch();
        rangedCircleAttack.Launch();
        while (!growingZoneAttack.IsAvailable()) { yield return null; }
        growingZoneAttack.Launch();
        while (!growingZoneAttack.IsAvailable() || !rangedCircleAttack.IsAvailable()) { yield return null; }
        yield return new WaitForSeconds(0.7f);
        isAttacking = false;
    }

    private IEnumerator Pattern2()
    {
        lavaThrowAttack.Launch();
        lavaThrowAttack.Launch();
        growingZoneAttack.Launch();
        yield return new WaitForSeconds(growingZoneAttackEstimatedTime/2);
        lavaThrowAttack.Launch();
        while (!growingZoneAttack.IsAvailable()) { yield return null; }
        yield return new WaitForSeconds(0.7f);
        isAttacking = false;
    }

    private IEnumerator Pattern3()
    {
        rangedCircleAttack.Launch();
        lavaThrowAttack.Launch();
        yield return new WaitForSeconds(3);
        lavaThrowAttack.Launch();
        yield return new WaitForSeconds(3);
        lavaThrowAttack.Launch();
        while (!rangedCircleAttack.IsAvailable()) { yield return null; }
        yield return new WaitForSeconds(0.7f);
        isAttacking = false;
    }

    private IEnumerator Pattern4()
    {
        growingZoneAttack.Launch();
        while (!growingZoneAttack.IsAvailable()) { yield return null; }
        yield return new WaitForSeconds(0.7f);
        isAttacking = false;
    }

    private IEnumerator Pattern5()
    {
        rangedCircleAttack.Launch();
        while (!rangedCircleAttack.IsAvailable()) { yield return null; }
        yield return new WaitForSeconds(0.7f);
        isAttacking = false;
    }

    private IEnumerator ShieldTime()
    {
        isProtected = true;
        shield.SetActive(true);

        shieldLeft--;
        hpToWatch = baseHP * (shieldLifeFraction * shieldLeft);

        int positionIndex = GetPositionIndex();

        while (!growingZoneAttack.IsAvailable() || !rangedCircleAttack.IsAvailable()) { yield return null; }

        //For first frame
        bossAnimator.SetFloat("Move X", positions[positionIndex].position.x - transform.position.x);
        bossAnimator.SetFloat("Move Y", positions[positionIndex].position.y - transform.position.y);
        bossAnimator.SetBool("Move", true);

        yield return new WaitForSeconds(0.2f);
        lavaThrowAttack.Launch();
        while (transform.position != positions[positionIndex].position)
        {
            bossAnimator.SetFloat("Move X", positions[positionIndex].position.x - transform.position.x);
            bossAnimator.SetFloat("Move Y", positions[positionIndex].position.y - transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, positions[positionIndex].position, 2 * Time.deltaTime);
            yield return null;
        }
        lavaThrowAttack.Launch();
        bossAnimator.SetBool("Move", false);
        yield return new WaitForSeconds(1f);
        lavaThrowAttack.Launch();
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

    public override void Harm(float ammount, float overtimeDamage)
    {
        if (isProtected) return;
        base.Harm(ammount, overtimeDamage);
        HPbar.UpdateHp(hp, baseHP);
    }

    public override void Die()
    {
        base.Die();
        Scaling.instance.ScalingIncrease();
        drops.BossDrop(transform.position, boss);
    }

    private void OnEnable()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        isAttacking = true;
        yield return new WaitForSeconds(2f);
        isAttacking = false;
    }

    protected override void Drop()
    {
    }

    protected override void WasPoisonHurt()
    {
        HPbar.UpdateHp(hp, baseHP);
    }
}
