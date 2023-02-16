using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class AnimatedFollow : MonoBehaviour
{
    [SerializeField] private bool useNavMesh;
    [SerializeField] private float speed;
    [SerializeField] private float speedIncrease;
    private BasicLevelManager manager;
    private Transform objective;
    private Animator animator;
    private Sensor sensor;
    private bool canMove = false;
    private ISensor<Player> playerSensor;
    private NavMeshAgent agent;

    [SerializeField] private float timeToAccelerate;
    private float initialTime;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
        DeactivateSensor();
    }
    private void Start()
    {
        if (useNavMesh)
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            agent.speed = speed;
        }
        initialTime = timeToAccelerate;
    }

    public void StartChasing(Transform player, BasicLevelManager basicLevelManager)
    {
        manager = basicLevelManager;
        objective = player;
        animator.SetBool("Move", true);
        canMove = true;
        ActivateSensor();
    }

    void Update()
    {
        if (!canMove) return;
        animator.SetFloat("Move X", objective.transform.position.x - transform.position.x);
        animator.SetFloat("Move Y", objective.transform.position.y - transform.position.y);

        Acceleration();

        if (useNavMesh)
        {
            agent.SetDestination(objective.position);
        }
        else 
        {
            transform.position = Vector2.MoveTowards(transform.position, objective.position, speed * Time.deltaTime);
        }
    }

    private void Acceleration()
    {
        timeToAccelerate -= Time.deltaTime;

        if (timeToAccelerate <= 0)
        {
            timeToAccelerate = initialTime;

            if (useNavMesh)
            {
                agent.speed += speedIncrease;
                agent.acceleration += speedIncrease;
            }
            else
            {
                speed += speedIncrease;
            }
        }
    }

    private void OnPlayerUnsense(Player otherObject) { }

    private void OnPlayerSense(Player otherObject)
    {
        canMove = false;
        manager.FollowingBossTouched();
    }

    void ActivateSensor()
    {
        sensor.gameObject.SetActive(true);
    }
    void DeactivateSensor()
    {
        sensor.gameObject.SetActive(false);
    }
}
