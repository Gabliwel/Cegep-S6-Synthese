using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMountainRock : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float speed;
    [SerializeField] private Vector2 destination;
    [SerializeField] private bool moving;
    [SerializeField] private bool breaking;

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
    private void OnEnable()
    {
        moving = false;
        animator.SetTrigger("Formation");
    }

    void OnPlayerSense(Player player)
    {
        if (player.Harm(damage))
            BreakRock();
    }

    void OnPlayerUnsense(Player player)
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
            transform.position = Vector2.MoveTowards(transform.position, destination, speed);
        if (DestinationReached() && !breaking)
        {
            BreakRock();
        }
    }

    public void SetDestination(Vector2 newDest)
    {
        destination = newDest;
    }

    bool DestinationReached()
    {
        return (Vector2)transform.position == destination;
    }

    void BreakRock()
    {
        moving = false;
        animator.SetBool("Spin", false);
        animator.SetTrigger("Break");
        breaking = true;
    }

    public void Formed()
    {
        animator.SetBool("Spin", true);
        moving = true;
    }

    public void RockBreakEnd()
    {
        breaking = false;
        gameObject.SetActive(false);
    }
}
