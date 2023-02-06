using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaShockWaveController : MonoBehaviour
{
    CircleCollider2D collider;
    LavaShockWaveMaskController maskController;
    // Start is called before the first frame update
    void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
        maskController = GetComponentInChildren<LavaShockWaveMaskController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += transform.localScale * Time.deltaTime/3;
        //collider.radius += Time.deltaTime/3 ;
        //maskController.Grow(Time.deltaTime, 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
