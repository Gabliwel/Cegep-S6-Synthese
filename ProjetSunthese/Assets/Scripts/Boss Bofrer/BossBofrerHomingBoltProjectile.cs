using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrerHomingBoltProjectile : Projectile
{
    [SerializeField] private float rotationSpeed;
    private Sensor sensor;
    private ISensor<Player> playerSensor;
    private BossBofrerHomingBolt parentBolt;

    private void Awake()
    {
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnSense;
        parentBolt = GetComponentInParent<BossBofrerHomingBolt>();
        damage = Scaling.instance.CalculateDamageOnScaling(damage);
    }

    private void OnPlayerSense(Player player)
    {
        if (player.Harm(damage))
            DestinationReached();
    }
    private void OnPlayerUnSense(Player player)
    {

    }
    protected override void Update()
    {
        transform.position += speed * Time.deltaTime * transform.right;
        AdjustRotation();
        if (ttlTimer > 0)
            ttlTimer -= Time.deltaTime;
        else
        {
            DestinationReached();
        }
    }

    private void AdjustRotation()
    {
        Vector2 targetDirection = Player.instance.transform.position - transform.position;

        targetDirection.Normalize();

        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    protected override void DestinationReached()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        parentBolt.ProjectileReached();
        base.DestinationReached();
    }
}
