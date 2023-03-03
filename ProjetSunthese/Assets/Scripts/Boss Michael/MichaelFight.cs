using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MichaelFight : Enemy
{
    [SerializeField] GameObject player; 
    [SerializeField] private List<BossAttack> michaelAttacks;
    private float attackSpeed = 1;

    private string[] Attacks = new string[3];
    private string currentAttack;
    private int attackNb = 0;

    private bool attackInProgress = false;

    private float attackDelay = 3f;

    private float SPEED = 3f;

    private Animator animator;

    private BossInfoController bossInfo;
    private const string bossName = "Michael";

    private Sensor damageSensor;
    private ISensor<Player> playerDamageSensor;
    private bool firstSpawn = true;

    protected override void Awake()
    {
        base.Awake();
        bossInfo = GameObject.FindGameObjectWithTag("BossInfo").GetComponent<BossInfoController>();
        bossInfo.SetName(bossName);
        bossInfo.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        bossInfo.gameObject.SetActive(false);
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        Attacks[0] = "CHARGE";
        Attacks[1] = "TELEPORT";
        Attacks[2] = "PROJECTILE";
        player = GameObject.FindGameObjectWithTag("Player");

        hp = Scaling.instance.CalculateHealthOnScaling(baseHP);
        damageDealt = Scaling.instance.CalculateDamageOnScaling(baseDamageDealt);

        for (int i = 0; i < michaelAttacks.Count; i++)
        {
            michaelAttacks[i] = Instantiate(michaelAttacks[i]);
            michaelAttacks[i].transform.SetParent(gameObject.transform);
        }

        damageSensor = transform.Find("Sensor").GetComponent<Sensor>();

        playerDamageSensor = damageSensor.For<Player>();

        playerDamageSensor.OnSensedObject += OnPlayerDamageSense;
        playerDamageSensor.OnUnsensedObject += OnPlayerDamageUnsense;

    }

    // Update is called once per frame
    void Update()
    {
        if (!attackInProgress)
        {
            attackDelay -= Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * SPEED);
            CheckAnimationSide();
            if (attackDelay <= 0)
            {
                attackDelay = 3f;
                attackInProgress = true;
                attackNb = Random.Range(0, 3);
                currentAttack = Attacks[attackNb];
                ExecuteCurrentAttack();
            }
        }
        else
        {
            if (michaelAttacks[attackNb].IsAvailable())
            {
                ResetAttackState();
            }
        }
    }

    private void CheckAnimationSide()
    {
        animator.SetFloat("Move X", player.transform.position.x - transform.position.x);
        animator.SetFloat("Move Y", player.transform.position.y - transform.position.y);
    }

    private void ExecuteCurrentAttack()
    {
            switch (currentAttack)
            {
                case "CHARGE":
                    StartFadedCharge();
                    break;
                case "TELEPORT":
                    StartTeleportUnder();
                    break;
                case "PROJECTILE":
                    StartProjectile();
                    break;
            }
        SoundMaker.instance.MichealTPSound(transform.position);
    }

    public void ResetAttackState()
    {
        attackInProgress = false;
        attackDelay = 3f;
    }

    private void StartFadedCharge()
    {
        michaelAttacks[0].Launch();
    }

    private void StartProjectile()
    {
        michaelAttacks[2].Launch();
    }


    private void StartTeleportUnder()
    {
        michaelAttacks[1].Launch();
    }

    protected override void Drop()
    {
        player.GetComponent<Player>().GainDrops(xpGiven, goldDropped);

        GetComponent<BossDrops>().BossDrop(transform.position);
    }

    public override void Die()
    {
        base.Die();
        Scaling.instance.ScalingIncrease();
        AchivementManager.instance.KilledMichael();
        bossInfo.Stop();
        bossInfo.gameObject.SetActive(false);
    }

    public override void Harm(float ammount, float poison)
    {
        base.Harm(ammount, poison);
        bossInfo.Bar.UpdateHealth(hp, baseHP);
    }

    protected override void WasPoisonHurt()
    {
        bossInfo.Bar.UpdateHealth(hp, baseHP);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        bossInfo.gameObject.SetActive(true);
        bossInfo.Bar.SetDefault(hp, baseHP);
        if (!firstSpawn)
        {
            StartCoroutine(AttackPlayerInRange());
        }
        firstSpawn = false;
    }


    private IEnumerator AttackPlayerInRange()
    {
        while (isActiveAndEnabled)
        {
            if (TouchingPlayer())
            {
                Player.instance.Harm(damageDealt);
                yield return new WaitForSeconds(attackSpeed);
            }
            yield return null;
        }
    }

    private bool TouchingPlayer()
    {
        Debug.Log(playerDamageSensor.SensedObjects.Count > 0);
        return playerDamageSensor.SensedObjects.Count > 0;
    }

    void OnPlayerDamageSense(Player player)
    {

    }

    void OnPlayerDamageUnsense(Player player)
    {

    }

    public float SendDamage()
    {
        return damageDealt;
    }
}
