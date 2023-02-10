using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    private float speedIncreaseTime = 10f;

    private GameObject boss;

    private NavMeshAgent agent;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        boss = GameObject.FindGameObjectWithTag("Boss");
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.transform.position = new Vector3(41, 92, 0);
            gameObject.SetActive(false);
            boss.GetComponent<MichaelFight>().enabled = true;
        }
    }
}
