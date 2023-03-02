using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private bool followBilly = false;
    private Transform player;
    private void Start()
    {
        if(!followBilly)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Billy").transform;
        }
    }
    void FixedUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -10);
    }
}
