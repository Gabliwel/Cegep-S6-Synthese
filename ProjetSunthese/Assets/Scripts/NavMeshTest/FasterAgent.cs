using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FasterAgent : MonoBehaviour
{
    [SerializeField] private float timeInterval;
    [SerializeField] private float gainedPercentSpeed;

    private NavMeshAgent agent;
    private float time;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent.speed > 10) return;

        time -= Time.deltaTime;
        if(time <= 0)
        {
            time = timeInterval;
            float toAdd = agent.speed * gainedPercentSpeed;
            agent.speed += toAdd;
            agent.acceleration = agent.speed * 5;
            Debug.Log(Time.unscaledTime + "(Current speed: " + agent.speed + ", added speed: " + toAdd + ")");
        }
    }
}
