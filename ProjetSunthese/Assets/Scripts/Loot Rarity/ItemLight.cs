using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLight : MonoBehaviour
{
    [Header("Local scale change")]
    [SerializeField] private float min = 0.5f;
    [SerializeField] private float max = 0.8f;
    [SerializeField] private float speed = 0.5f;

    void Update()
    {
        transform.localScale = new Vector3(Mathf.PingPong(Time.time * speed, (max - min)) + min, Mathf.PingPong(Time.time * speed, (max - min)) + min, 1);
    }
}
