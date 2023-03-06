using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BofTeleport : BossAttack
{
    private Player player;
    [SerializeField] GameObject dangerCircle;
    [SerializeField] GameObject shadowClone;
    [SerializeField] float damage;
    [SerializeField] float durationReset = 1.5f;

    private bool attackInProgress = false;

    private float reactionTime = 2.5f;
    private float duration;

    private bool aoeOnce = true;

    private Sensor sensor;
    private ISensor<Player> playerSensor;

    [ContextMenu("Test")]
    public override void Launch()
    {
        isAvailable = false;
        attackInProgress = true;
        duration = durationReset;
    }

    // Start is called before the first frame update
    void Awake()
    {
        shadowClone = Instantiate(shadowClone);
        dangerCircle = Instantiate(dangerCircle);
        sensor = shadowClone.GetComponentInChildren<Sensor>(true);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
        type = BossAttackType.Michael;
        damage = Scaling.instance.CalculateDamageOnScaling(damage);
    }

    void OnPlayerSense(Player player)
    {
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
        if (shadowClone.activeSelf)
        {
            duration -= Time.deltaTime;

            if(duration <= 0)
            {
                shadowClone.SetActive(false);
                isAvailable = true;
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
                    shadowClone.transform.position = dangerCircle.transform.position;
                    shadowClone.SetActive(true);
                    dangerCircle.SetActive(false);
                    reactionTime = 1.5f;
                    attackInProgress = false;
                    aoeOnce = true;
                }
            }
        }
    }
}
