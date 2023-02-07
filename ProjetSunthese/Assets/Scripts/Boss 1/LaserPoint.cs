using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPoint : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void StartMovement(Vector3 pivotPoint)
    {
        StartCoroutine(Rotate(pivotPoint));
    }

    private IEnumerator Rotate(Vector3 pivotPoint)
    {
        while (true)
        {
            transform.RotateAround(pivotPoint, new Vector3(0, 0, 1), Time.deltaTime * 50);
            yield return null;
        }
    }
}
