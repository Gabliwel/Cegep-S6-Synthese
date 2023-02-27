using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    [SerializeField] private float fuse;
    private float fuseTimer;
    private bool fuseStarted;
    private bool redStarted;
    private BombHolder parentHolder;
    private Animator animator;
    private Sensor sensor;
    private ISensor<Player> playerSensor;

    private void Awake()
    {
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
        parentHolder = GetComponentInParent<BombHolder>();
        animator = GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        fuseStarted = false;
        redStarted = false;
        animator.SetTrigger("Roll");
    }

    protected override void Update()
    {
        base.Update();
        if (fuseStarted)
        {
            if (fuseTimer > 0)
                fuseTimer -= Time.deltaTime;
            else
                StartRed();
        }

    }

    private void OnPlayerSense(Player player)
    {
        destination = transform.position;
        DestinationReached();
    }
    private void OnPlayerUnsense(Player enemy)
    {

    }

    protected override void DestinationReached()
    {
        StartFuse();
    }

    private void StartFuse()
    {
        if (fuseStarted) return;
        Debug.Log("starting fuse");
        fuseTimer = fuse;
        fuseStarted = true;
        animator.SetTrigger("Idle");
    }

    private void StartRed()
    {
        if (redStarted) return;
        redStarted = true;
        animator.SetTrigger("Red");
    }

    public void Explode()
    {
        parentHolder.ProjectileReached();
        base.DestinationReached();
    }
}
