using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaAura : BossAttack
{
    [SerializeField] private float damage;
    float timeElapsed = 0;
    private Sensor sensor;
    private ISensor<Player> playerSensor;
    private ISensor<EnemyLavaController> enemySensor; 
    private Player player;
    [SerializeField] private float damageTicksTimer;
    private ParticleSystem particleSystem;
    private bool activate = false;
    private Collider2D auraCollider;
    private float timer = 0f;
    private Coroutine tickCoroutine;
    private Coroutine auraCoroutine;
    private int tickDamage;
    void Awake()
    {
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        enemySensor = sensor.For<EnemyLavaController>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
        enemySensor.OnSensedObject += OnEnemySense;
        enemySensor.OnSensedObject += OnEnemyUnsense;
        particleSystem = GetComponentInChildren<ParticleSystem>();
        sensor.enabled = false;
        type = BossAttackType.Lava;
    }

    private IEnumerator StartAura()
    {
        ActivateAura();
        SoundMaker.instance.GontrandAuraSound(transform.position);
        timer = 10f;
        yield return new WaitForSeconds(timer);
        DespawnAura();
        SoundMaker.instance.StopFireAuraSound();
        StopCoroutine(auraCoroutine);
    }

    public void DespawnAura()
    {
        sensor.enabled = false;
        particleSystem.Stop();
    }

    private void ActivateAura()
    {
        sensor.enabled = true;
        particleSystem.Play();
    }

    public override void Launch()
    {
        auraCoroutine = StartCoroutine(StartAura());
    }

    IEnumerator Tick(Player player)
    {
        while (isActiveAndEnabled)
        {
            player.Harm(damage);
            yield return new WaitForSeconds(damageTicksTimer);
        }
    }

    #region Sensor

    void OnEnemySense(EnemyLavaController enemy)
    {
        enemy.Ascend();
    }

    void OnEnemyUnsense(EnemyLavaController enemy)
    {

    }

    void OnPlayerSense(Player player)
    {
        tickCoroutine = StartCoroutine(Tick(player));
    }

    void OnPlayerUnsense(Player player)
    {
        StopCoroutine(tickCoroutine);
    }

    #endregion
}
