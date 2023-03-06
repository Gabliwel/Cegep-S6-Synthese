using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilleMovement : MonoBehaviour
{
    private Player player;
    private Vector3 playerPos;
    private Sensor sensor;
    private ISensor<Player> playerSensor;
    private float damage;
    void Start()
    {
        sensor = GetComponentInChildren<Sensor>(true);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
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
        transform.position = Vector2.MoveTowards(transform.position, playerPos, 0.1f);

        if(transform.position == playerPos)
        {
            gameObject.SetActive(false);
        }
    }

    public void SetDestination(Vector3 destination)
    {
        gameObject.SetActive(true);

        playerPos = destination;
    }

    public void SetDamage(float damageBoss)
    {
        damage = damageBoss;
    }
}
