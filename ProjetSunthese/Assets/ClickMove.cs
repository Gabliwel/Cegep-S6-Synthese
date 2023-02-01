using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMove : MonoBehaviour
{
    private Vector3 target;

    void Start()
    {
        target = new Vector3(-11, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, 1f);
    }
}
