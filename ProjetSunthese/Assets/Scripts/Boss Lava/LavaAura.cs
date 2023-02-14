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
        auraCollider = sensor.GetComponent<Collider2D>();
        auraCollider.enabled = false;
    }

    private IEnumerator StartAura()
    {
        ActivateAura();
        timer = 10f;
        yield return new WaitForSeconds(timer);
        DespawnAura();
        StopCoroutine(auraCoroutine);
    }

    public void DespawnAura()
    {
        auraCollider.enabled = false;
        particleSystem.Stop();
    }

    private void ActivateAura()
    {
        
        particleSystem.Play();
        auraCollider.enabled = true;
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
