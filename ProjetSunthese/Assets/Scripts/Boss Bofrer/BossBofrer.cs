using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrer : Enemy
{
    private const int HP_THRESHOLD_SCALING_CUTOFF = 5;
    private const int ACTIVE_BFL_SCALE = 0;
    private const int ACTIVE_SHIELD_MINION_SCALE = 2;
    private const int ACTIVE_BOLT_SCALE = 3;
    private const int ACTIVE_BALL_SCALE = 4;
    [SerializeField] private float HPTreshold;
    [Header("Attack Steal")]
    [SerializeField] private List<BossAttack> stolenAttacks;
    [SerializeField] private float stolenMinTimer;
    [Header("BFL")]
    [SerializeField] private BossBofrerBFL bflPrefab;
    [SerializeField] private float bflChargeup;
    [SerializeField] private float bflminTimer;
    [Header("Shield Minions")]
    [SerializeField] private BossBofrerShieldMinionSpawner shieldMinionSpawnerPrefab;
    [SerializeField] private bool shieldActive;
    [SerializeField] private float shieldMinionsMinTimer;
    [Header("Homing Bolts")]
    [SerializeField] private BossBofrerHomingBoltSpawner boltSpawnerPrefab;
    [SerializeField] private float boltMinTimer;
    [Header("Ball")]
    [SerializeField] private BossBofrerBall ballPrefab;
    [SerializeField] private float ballMinTimer;
    private bool boltsActive;
    private bool bflActive;
    private bool ballActive;
    private bool shieldMinionsActive;
    private bool stolenActive;
    private bool hasRespawned = false;
    private bool routinesStarted;

    private BossBofrerHomingBoltSpawner boltSpawner;
    private BossBofrerShieldMinionSpawner minionSpawner;
    private BossBofrerBFL bfl;
    private BossBofrerBall ball;
    private BofrerRevisitsManger revisitsManager;
    private Animator animator;
    private GameObject shield;
    private HPBar hpBar;

    private void Awake()
    {
        bfl = Instantiate(bflPrefab);
        bfl.transform.position = transform.position;
        bfl.gameObject.SetActive(false);

        minionSpawner = Instantiate(shieldMinionSpawnerPrefab);
        minionSpawner.transform.position = transform.position;

        boltSpawner = Instantiate(boltSpawnerPrefab);
        boltSpawner.transform.position = transform.position;

        ball = Instantiate(ballPrefab);
        ball.transform.position = transform.position;

        shield = transform.Find("Shield").gameObject;
        shield.SetActive(false);

        animator = GetComponent<Animator>();
        revisitsManager = GetComponent<BofrerRevisitsManger>();
        hpBar = GetComponentInChildren<HPBar>();
    }

    protected override void Start()
    {
        base.Start();
        stolenAttacks = revisitsManager.GetStolenAttacks();
        ActivateAttacks();
        EnsureRoutinesStarted();
        HPTreshold = CalculateHpThreshold();
        hp -= CalculateHpAtThreshold();
        hpBar.UpdateHp(hp, Scaling.instance.CalculateHealthOnScaling(baseHP));
    }

    void ActivateAttacks()
    {
        int scaling = Scaling.instance.SendScaling();

        bflActive = scaling >= ACTIVE_BFL_SCALE;
        ballActive = scaling >= ACTIVE_BALL_SCALE;
        shieldMinionsActive = scaling >= ACTIVE_SHIELD_MINION_SCALE;
        boltsActive = scaling >= ACTIVE_BOLT_SCALE;

        stolenActive = stolenAttacks.Count != 0;
    }

    void EnsureRoutinesStarted()
    {
        if (routinesStarted) return;

        if (bflActive)
            StartCoroutine(BFLTimerRoutine());
        if (ballActive)
            StartCoroutine(BallTimerRoutine());
        if (boltsActive)
            StartCoroutine(BoltTimerRoutine());
        if (shieldMinionsActive)
            StartCoroutine(ShieldMinionTimerRoutine());
        if (stolenActive)
            StartCoroutine(StolenAttackTimerRoutine());
        routinesStarted = true;
    }

    private void Update()
    {
        shieldActive = minionSpawner.AnyMinionActive();
        shield.SetActive(shieldActive);
    }

    protected override void Drop()
    {
    }

    private float CalculateHpThreshold()
    {
        float scaling = Scaling.instance.SendScaling();

        float cutoff = scaling / HP_THRESHOLD_SCALING_CUTOFF;
        return hp - (hp * cutoff);
    }

    private float CalculateHpAtThreshold()
    {
        float scaling = Scaling.instance.SendScaling();
        return HP_THRESHOLD_SCALING_CUTOFF - (scaling - 1 / HP_THRESHOLD_SCALING_CUTOFF);
    }

    public override void Harm(float ammount, float overtime)
    {
        if (!shieldActive)
        {
            base.Harm(ammount, overtime);
            CheckHPForTeleport();
            hpBar.UpdateHp(hp, Scaling.instance.CalculateHealthOnScaling(baseHP));
        }
    }

    protected override void WasPoisonHurt()
    {
        base.WasPoisonHurt();
        if (!shieldActive)
        {
            CheckHPForTeleport();
            hpBar.UpdateHp(hp, Scaling.instance.CalculateHealthOnScaling(baseHP));
        }
    }

    private void CheckHPForTeleport()
    {
        if (hp < HPTreshold && !IsFinalFight())
        {
            GameManager.instance.SetNextLevel();
        }
    }

    public override void Die()
    {
        if (hasRespawned)
        {
            base.Die();
        }
        else
        {
            hasRespawned = true;
            hp = Scaling.instance.CalculateHealthOnScaling(baseHP);
        }
    }

    private bool IsFinalFight()
    {
        return Scaling.instance.SendScaling() > HP_THRESHOLD_SCALING_CUTOFF + 1;
    }

    IEnumerator BFLTimerRoutine()
    {
        while (isActiveAndEnabled && bflActive)
        {
            float timer = GetRandomTimer(bflminTimer);
            yield return new WaitForSeconds(timer);
            StartBFL();
        }
    }
    IEnumerator BallTimerRoutine()
    {
        while (isActiveAndEnabled && ballActive)
        {
            float timer = GetRandomTimer(ballMinTimer);
            yield return new WaitForSeconds(timer);
            StartBallAttack();
        }
    }

    IEnumerator BoltTimerRoutine()
    {
        while (isActiveAndEnabled && boltsActive)
        {
            float timer = GetRandomTimer(boltMinTimer);
            yield return new WaitForSeconds(timer);
            StartBoltSpawn();
        }
    }

    IEnumerator ShieldMinionTimerRoutine()
    {
        while (isActiveAndEnabled && shieldMinionsActive)
        {
            float timer = GetRandomTimer(shieldMinionsMinTimer);
            yield return new WaitForSeconds(timer);
            StartMinionSpawn();
        }
    }

    IEnumerator StolenAttackTimerRoutine()
    {
        while (isActiveAndEnabled && stolenActive)
        {
            float timer = GetRandomTimer(stolenMinTimer);
            yield return new WaitForSeconds(timer);
            StartRandomStolenAttack();
        }
    }

    float GetRandomTimer(float minTime)
    {
        float maxTime = minTime * 1.5f;

        float random = Random.Range(minTime, maxTime);
        return random;
    }



    void StartRandomStolenAttack()
    {
        int num = Random.Range(0, stolenAttacks.Count);
        if (stolenAttacks[num].IsAvailable())
        {
            stolenAttacks[num].transform.position = transform.position;
            stolenAttacks[num].gameObject.SetActive(true);
            stolenAttacks[num].Launch();
        }
        Debug.Log("firering " + stolenAttacks[num].name + " number " + num);
    }

    void StartBFL()
    {
        StartCoroutine(BFLRoutine());
    }

    void StartMinionSpawn()
    {
        minionSpawner.Launch();
    }

    void StartBoltSpawn()
    {
        boltSpawner.Launch();
    }

    void StartBallAttack()
    {
        ball.Launch();
    }

    IEnumerator BFLRoutine()
    {
        animator.SetBool("Charging", true);
        yield return new WaitForSeconds(bflChargeup);
        bfl.Launch();
        animator.SetBool("Charging", false);
    }
}
