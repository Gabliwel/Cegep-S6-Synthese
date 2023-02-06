using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    private void Update()
    {
        Vector3 pos = gameObject.transform.position;
        float newY = Mathf.Sin(Time.time * 2) - 1.5f;
        transform.position = new Vector2(pos.x, newY * 0.2f);
    }
}
