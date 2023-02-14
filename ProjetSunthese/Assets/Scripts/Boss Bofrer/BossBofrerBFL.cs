using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrerBFL : BossAttack
{
    [SerializeField] private float damage;
    [SerializeField] private float tickRate;
    [SerializeField] private float ttl;
    private float timer;
    private Sensor sensor;
    private ISensor<Player> playerSensor;
    private Coroutine tickCoroutine;

    private void Awake()
    {
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
        type = BossAttackType.Bofrer;
    }
    private void OnEnable()
    {
        timer = ttl;
    }

    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
            gameObject.SetActive(false);
    }

    public override void Launch()
    {
        gameObject.SetActive(true);
    }
    void OnPlayerSense(Player player)
    {
        tickCoroutine = StartCoroutine(Tick(player));
    }

    void OnPlayerUnsense(Player player)
    {
        StopCoroutine(tickCoroutine);
    }

    IEnumerator Tick(Player player)
    {
        while(isActiveAndEnabled)
        {
            player.Harm(damage);
            yield return new WaitForSeconds(tickRate);
        }
    }
}
