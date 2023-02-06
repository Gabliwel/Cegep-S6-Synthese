using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RunAway : MonoBehaviour
{
    [SerializeField] private GameObject target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 runTo = transform.position + ((transform.position - target.transform.position));
        agent.SetDestination(runTo);
    }
}
