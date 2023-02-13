using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrerEarly : Enemy
{
    [SerializeField] private float speed;
    Player player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    protected override void Drop()
    {

    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed);
    }
}
