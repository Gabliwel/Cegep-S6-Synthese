using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMountainStalagmite : MonoBehaviour
{
    [SerializeField] private float damage;

    private BossMountain bossMountain;
    private Animator animator;

    private Sensor sensor;
    private ISensor<Player> playerSensor;

    private void Awake()
    {
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
        bossMountain = GetComponentInParent<BossMountain>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        
    }

    private void OnEnable()
    {
        animator.SetTrigger("Rise");
    }

    public void EnableSensor()
    {
        sensor.gameObject.SetActive(true);
    }

    public void DisableSensor()
    {
        sensor.gameObject.SetActive(false);
    }

    public void EnteredGround()
    {
        gameObject.SetActive(false);
    }

    void OnPlayerSense(Player player)
    {
        if (player.Harm(damage))
            DisableSensor();
    }

    void OnPlayerUnsense(Player player)
    {

    }

    
}
