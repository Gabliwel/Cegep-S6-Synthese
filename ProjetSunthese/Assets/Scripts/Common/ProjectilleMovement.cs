using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilleMovement : MonoBehaviour
{
    private Vector3 playerPos;
    void Start()
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
}
