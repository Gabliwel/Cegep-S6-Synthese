using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaShockWaveController : MonoBehaviour
{
    CircleCollider2D collider;
    // Start is called before the first frame update
    void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += transform.localScale * Time.deltaTime/3;
        collider.radius += Time.deltaTime/3 ;
    }
}
