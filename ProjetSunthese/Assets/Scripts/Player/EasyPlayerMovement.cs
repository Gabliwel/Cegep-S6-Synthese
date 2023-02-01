using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyPlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer sprite;

    [SerializeField] private float speed = 3;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 dir = Vector2.zero;

        if (Input.GetKey(KeyCode.A))
        {
            dir.x = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            dir.x = 1;
        }

        if (Input.GetKey(KeyCode.W))
        {
            dir.y = 1;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            dir.y = -1;
        }

        dir.Normalize();
        body.velocity = speed * dir;
    }
}
