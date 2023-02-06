using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClosestSpot : MonoBehaviour
{
    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TakeCover();
    }

    void TakeCover()
    {
        NavMeshHit hit;
        if (agent.FindClosestEdge(out hit))
            agent.SetDestination(hit.position);

    }
}
