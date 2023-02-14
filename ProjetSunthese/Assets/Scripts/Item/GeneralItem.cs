using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralItem : MonoBehaviour
{
    protected float time = 1f;

    void Update()
    {
        if (time >= 0)
        {
            time -= Time.deltaTime;

            if (time <= 0)
            {
                GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    private void OnEnable()
    {
        GetComponent<Collider2D>().enabled = false;
        time = 1f;
    }
}
