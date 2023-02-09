using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMountainStalagmite : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float warningTime;
    [SerializeField] private bool isWarning;
    [SerializeField] private float waitTime;
    [SerializeField] private bool isWaiting;

    private float warningTimer;
    private float waitTimer;

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
        if (warningTimer > 0)
            warningTimer -= Time.deltaTime;
        else if (isWarning)
        {
            DisableWarning();
            animator.SetTrigger("Rise");
        }

        if (waitTimer > 0)
            waitTimer -= Time.deltaTime;
        else if (isWaiting)
        {
            isWaiting = false;
            animator.SetTrigger("Fall");
        }
    }

    private void OnEnable()
    {
        EnableWarning();
    }

    void EnableWarning()
    {
        isWarning = true;
        animator.SetBool("Warning", true);
        warningTimer = warningTime;
    }

    void DisableWarning()
    {
        isWarning = false;
        animator.SetBool("Warning", false);
        warningTimer = 0;
    }

    public void EnableSensor()
    {
        sensor.gameObject.SetActive(true);
    }

    public void DisableSensor()
    {
        sensor.gameObject.SetActive(false);
    }

    public void FinishedRising()
    {
        DisableSensor();
        isWaiting = true;
        waitTimer = waitTime;
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

    //required to make sensor work, no use case here
    void OnPlayerUnsense(Player player)
    {

    }
}
