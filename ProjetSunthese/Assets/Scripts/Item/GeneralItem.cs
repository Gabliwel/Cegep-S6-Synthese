using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneralItem : MonoBehaviour
{
    protected GameObject player;
    protected float time = 1f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(time >= 0)
        {
            time -= Time.deltaTime;

            if(time <= 0)
            {
                GetComponent<Collider2D>().enabled = true;
            }
        }
    }
}
