using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    private float speedIncreaseTime = 10f;

    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        agent.SetDestination(player.transform.position);

        speedIncreaseTime -= Time.deltaTime;

        if (speedIncreaseTime <= 0)
        {
            agent.speed += 0.5f;
            speedIncreaseTime = 10f;
        }
    }
}
