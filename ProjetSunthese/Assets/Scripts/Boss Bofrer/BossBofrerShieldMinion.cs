using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrerShieldMinion : Enemy
{
    [SerializeField] private float rotationSpeed;
    Transform childTransform;
    private Sensor sensor;
    private ISensor<Player> playerSensor;

    protected override void Awake()
    {
        base.Awake();
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
    }

    private void OnEnable()
    {
        childTransform = transform.GetChild(0).transform;
    }
    private void Update()
    {
        SetRotation(rotationSpeed * Time.deltaTime);
    }
    protected override void Drop()
    {
    }

    void OnPlayerSense(Player player)
    {
    }

    void OnPlayerUnsense(Player player)
    {

    }

    public void SetRotation(float ammount)
    {
        Vector3 rotation = new Vector3(0, 0, ammount);
        transform.Rotate(rotation);
        childTransform.Rotate(-rotation);
    }
}
