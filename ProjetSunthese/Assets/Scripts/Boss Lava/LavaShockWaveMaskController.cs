using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaShockWaveMaskController : MonoBehaviour
{
    CircleCollider2D collider;
    public bool playerIsInSafeZone = false;
    // Start is called before the first frame update
    void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            playerIsInSafeZone = true;
            Debug.Log(playerIsInSafeZone);
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            playerIsInSafeZone = false;
            Debug.Log(playerIsInSafeZone);
        }
    }
}
