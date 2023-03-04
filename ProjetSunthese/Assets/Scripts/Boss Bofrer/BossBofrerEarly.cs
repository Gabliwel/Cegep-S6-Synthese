using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrerEarly : Enemy
{
    [SerializeField] private float speed;
    [SerializeField] private float attackSpeed;
    private float hpThreshold;
    private const float SCALING_CUTOFF = 0.2f;
    private Player player;
    private Animator animator;
    private Sensor sensor;
    private ISensor<Player> playerSensor;
    private BossInfoController bossInfo;
    private const string bossName = "Bofrer";

    protected override void Awake()
    {
        base.Awake();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        animator = GetComponent<Animator>();
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnSense;
        bossInfo = GameObject.FindGameObjectWithTag("BossInfo").GetComponent<BossInfoController>();
        bossInfo.SetName(bossName);
        bossInfo.gameObject.SetActive(false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        animator.SetTrigger("Early");
        StartCoroutine(AttackPlayerInRange());
        bossInfo.gameObject.SetActive(true);
        bossInfo.Bar.SetDefault(hp, scaledHp);
    }

    private void OnDisable()
    {
        bossInfo.gameObject.SetActive(false);
    }

    private void Start()
    {
        hpThreshold = CalculateHpThreshold();
    }
    protected override void Drop()
    {
    }


    private void OnPlayerSense(Player player)
    {
    }
    private void OnPlayerUnSense(Player player)
    {

    }

    public override void Harm(float ammount, float overtimeDamage)
    {
        base.Harm(ammount, overtimeDamage);
        bossInfo.Bar.UpdateHealth(hp, scaledHp);
        CheckHPForTeleport();
    }

    protected override void WasPoisonHurt()
    {
        base.WasPoisonHurt();
        bossInfo.Bar.UpdateHealth(hp, scaledHp);
        CheckHPForTeleport();
    }

    private void Update()
    {
        if (!TouchingPlayer())
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        animator.SetFloat("MoveX", player.transform.position.x - transform.position.x);
        animator.SetFloat("MoveY", player.transform.position.y - transform.position.y);
    }

    private void CheckHPForTeleport()
    {
        if (hp < hpThreshold)
        {
            GameManager.instance.SetNextLevel();
        }
    }

    private float CalculateHpThreshold()
    {
        return hp - (hp * SCALING_CUTOFF);
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
        Debug.Log(playerSensor.SensedObjects.Count > 0);
        return playerSensor.SensedObjects.Count > 0;
    }

}
