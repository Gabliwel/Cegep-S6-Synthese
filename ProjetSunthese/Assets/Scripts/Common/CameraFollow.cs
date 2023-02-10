using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    public Vector3 offset;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //offset = new Vector3();
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, -20);
    }
}
