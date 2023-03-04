using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBillyMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool inCinema = false;

    private Animator animator;
    private Rigidbody2D rb;

    private Vector2 lastDir = Vector2.zero;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (inCinema) return;
        if (!canMove)
        {
            lastDir = Vector2.zero;
            return;
        }

        Vector2 movementInput = Vector2.zero;
        movementInput.x = Input.GetAxis("Horizontal");
        movementInput.y = Input.GetAxis("Vertical");
        movementInput.Normalize();

        lastDir = movementInput;
    }

    private void FixedUpdate()
    {
        if (inCinema) return;
        rb.velocity = lastDir * speed;

        if (lastDir == Vector2.zero)
        {
            animator.SetBool("Move", false);
            return;
        }

        animator.SetBool("Move", true);

        animator.SetFloat("Move X", lastDir.x);
        animator.SetFloat("Move Y", lastDir.y);
    }

    public void StopMove()
    {
        canMove = false;
    }

    public void MoveUpAnim()
    {
        animator.SetBool("Move", true);
        animator.SetFloat("Move X", 0);
        animator.SetFloat("Move Y", 1);
    }

    public void IdleAnim()
    {
        animator.SetBool("Move", false);
        animator.SetFloat("Move X", 0);
        animator.SetFloat("Move Y", 0);
    }
}
