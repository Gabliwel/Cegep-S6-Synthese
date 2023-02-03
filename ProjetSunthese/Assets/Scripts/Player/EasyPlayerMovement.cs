using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyPlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private SpriteRenderer sprite;
    private bool knockBack = false;

    [SerializeField] private float speed = 3;

    [Header("KnockBack")]
    [Range(1, 20)]
    [SerializeField] private float dragStopper = 8;
    [Range(0, 2)]
    [SerializeField] private float stopAtMagnitude = 1.25f;

    //for test
    private bool isRolling = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        body.drag = 0;
    }

    void Update()
    {
        if (knockBack) return;

        // for test ************
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isRolling = !isRolling;
            if (isRolling) sprite.color = Color.green;
            else sprite.color = Color.white;
        }
        //**********************


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

    public void AddKnockBack(Vector2 difference, float force)
    {
        if (isRolling || knockBack) return;

        knockBack = true;
        body.velocity = Vector2.zero;
        body.drag = dragStopper;

        body.AddForce(difference * force, ForceMode2D.Impulse);
        StartCoroutine(CheckKnockBack());
    }

    private IEnumerator CheckKnockBack()
    {
        while(body.velocity.magnitude > stopAtMagnitude)
        {
            yield return true;
        }
        body.velocity = Vector2.zero;
        knockBack = false;
        body.drag = 0;
    }
}
