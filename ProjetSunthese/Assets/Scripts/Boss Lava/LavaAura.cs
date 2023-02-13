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
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (activate && timer < 10)
        {
            ActivateAura();
        }
        else
        {
            DespawnAura();
        }
        damageTicksTimer += Time.deltaTime;
        if (player != null)
        {
            if(damageTicksTimer > 1)
            {
                player.Harm(damage);
                damageTicksTimer = 0;
            }
        }
        timeElapsed += Time.deltaTime;
        CalculateAuraTiming();
    }
    
    public void CalculateAuraTiming()
    {
        
        if (timeElapsed < 10)
        {
            ActivateAura();
        }
        else if(timeElapsed > 10)
        {
            DespawnAura();
            timeElapsed = 0;
        }   
        
    }

    public void DespawnAura()
    {
        auraCollider.enabled = false;
        particleSystem.Stop();
    }

    private void ActivateAura()
    {
        if(!particleSystem.isPlaying)
            particleSystem.Play();
        auraCollider.enabled = true;
    }

    public override void Launch()
    {
        throw new System.NotImplementedException();
    }

    #region Sensor
    void OnEnemySense(EnemyLavaController enemy)
    {
        Debug.Log("COde -1 9");
        enemy.Ascend();
    }


    void OnEnemyUnsense(EnemyLavaController enemy)
    {

    }


    void OnPlayerSense(Player player)
    {
        player.Harm(damage);
        this.player = player;
    }


    void OnPlayerUnsense(Player player)
    {
        this.player = null;
    }

    #endregion
}
