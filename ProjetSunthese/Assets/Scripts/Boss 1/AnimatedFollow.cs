using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedFollow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform objective;
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        animator.SetBool("Move", true);
    }

    void Update()
    {
        animator.SetFloat("Move X", objective.transform.position.x - transform.position.x);
        animator.SetFloat("Move Y", objective.transform.position.y - transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, objective.position, speed * Time.deltaTime);
    }
}
