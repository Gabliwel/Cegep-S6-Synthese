using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaAura : MonoBehaviour
{
    [SerializeField] private float damage;
    float timeElapsed = 0;
    private Sensor sensor;
    private ISensor<Player> playerSensor;
    private ISensor<EnemyLavaController> enemySensor; 
    private Player player;
    void Awake()
    {
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        enemySensor = sensor.For<EnemyLavaController>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
        enemySensor.OnSensedObject += OnEnemySense;
        enemySensor.OnSensedObject += OnEnemyUnsense;
    }

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

    void Update()
    {
        if (player != null)
        {
            player.Harm(damage);
        }
        timeElapsed += Time.deltaTime;
        CalculateAuraTiming();
    }
    
    public void CalculateAuraTiming()
    {
        if (timeElapsed > 10)
        {
            DespawnAura();
            timeElapsed = 0;
        }
    }

    public void DespawnAura()
    {
        gameObject.SetActive(false);
    }
}
