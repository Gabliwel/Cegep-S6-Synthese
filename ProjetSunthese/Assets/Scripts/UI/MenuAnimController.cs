using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.Rebind();
        animator.Update(0f);
    }
}
