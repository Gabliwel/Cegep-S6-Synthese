using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingClouds : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private bool moveLeft;
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float speed;

    private Vector3 target;

    void Start()
    {
        if (moveLeft)
        {
            target = Vector3.left;
        }
        else
        {
            target = Vector3.right;
        }
    }

    void Update()
    {
        transform.Translate(target * speed * Time.deltaTime * 0.5f);

        if (transform.position.x <= leftLimit)
        {
            target = Vector3.right;
        }
        else if (transform.position.x >= rightLimit)
        {
            target = Vector3.left;
        }
    }
}
