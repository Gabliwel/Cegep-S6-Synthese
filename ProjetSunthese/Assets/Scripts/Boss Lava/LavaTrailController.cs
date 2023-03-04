using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTrailController : MonoBehaviour
{
    [SerializeField] private float damageTicksTimer;
    [SerializeField] private int damage = 1;
    float timeElapsed = 0;
    private ISensor<Player> playerSensor; 
    private Sensor sensor;
    private Player player;
    private const int timeToLive = 49;

    void Start()
    {
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
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
        damageTicksTimer += Time.deltaTime;
        timeElapsed += Time.deltaTime;
        if (player != null)
        {
            if (damageTicksTimer > 1)
            {
                player.Harm(damage);
                damageTicksTimer = 0;
            }
        }

        if(timeElapsed > timeToLive)
        {
            gameObject.SetActive(false);
            timeElapsed = 0;
        }
        
    }

}
