using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatedFollow : MonoBehaviour
{
    [SerializeField] private float speed;
    private BasicLevelManager manager;
    private Transform objective;
    private Animator animator;
    private Sensor sensor;
    private bool canMove = false;
    private ISensor<Player> playerSensor;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
        DeactivateSensor();
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
        transform.position = Vector2.MoveTowards(transform.position, objective.position, speed * Time.deltaTime);
    }

    private void OnPlayerUnsense(Player otherObject) { }

    private void OnPlayerSense(Player otherObject)
    {
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
