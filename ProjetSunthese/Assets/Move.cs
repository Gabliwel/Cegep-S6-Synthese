using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Move : MonoBehaviour
{
    private Vector3 target;
    NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        target = new Vector3(6, 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target);
    }
}
