using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BofTeleport : BossAttack
{
    private Player player;
    [SerializeField] GameObject dangerCircle;
    [SerializeField] GameObject laserAoE;
    [SerializeField] float damage;

    private bool attackInProgress = false;

    private float reactionTime = 2.5f;
    private float duration = 1.5f;

    private bool aoeOnce = true;

    private Sensor sensor;
    private ISensor<Player> playerSensor;

    [ContextMenu("Test")]
    public override void Launch()
    {
        Debug.Log("Test");
        tempBossAttackReady = false;
        attackInProgress = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        sensor = laserAoE.GetComponentInChildren<Sensor>(true);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
    }

    void OnPlayerSense(Player player)
    {
        Debug.Log("Damage");
        player.Harm(damage);
    }

    void OnPlayerUnsense(Player player)
    {

    }

    void Update()
    {
        if (attackInProgress)
        {
            TeleportUnder();
        }
        if (laserAoE.activeSelf)
        {
            duration -= Time.deltaTime;

            if(duration <= 0)
            {
                laserAoE.SetActive(false);
                tempBossAttackReady = true;
            }
        }
    }

    private void TeleportUnder()
    {
        if (attackInProgress)
        {
            if (aoeOnce)
            {
                dangerCircle.SetActive(true);
                dangerCircle.transform.position = player.transform.position;
                aoeOnce = false;
            }

            if (!aoeOnce)
            {
                reactionTime -= Time.deltaTime;

                if (reactionTime <= 0)
                {
                    laserAoE.transform.position = dangerCircle.transform.position;
                    laserAoE.SetActive(true);
                    dangerCircle.SetActive(false);
                    reactionTime = 1.5f;
                    attackInProgress = false;
                }
            }
        }
    }
}
