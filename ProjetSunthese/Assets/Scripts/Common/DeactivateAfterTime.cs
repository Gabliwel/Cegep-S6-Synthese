using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateAfterTime : MonoBehaviour
{
    [SerializeField] private float totalTime;
    private float currentTime;
    void Start()
    {
        currentTime = totalTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0) gameObject.SetActive(false);
    }
}
