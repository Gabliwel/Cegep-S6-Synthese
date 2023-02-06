using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaShockWaveMaskController : MonoBehaviour
{
    public bool playerIsInSafeZone = false;
    CircleCollider2D collider;
    // Start is called before the first frame update
    void Awake()
    {
        collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Grow(float deltaTime, int dividedBy)
    {
        transform.localScale += new Vector3(0.005f, 0.005f, 0);
        collider.radius += 0.005f;
        //transform.localScale += transform.localScale * deltaTime/dividedBy;
        //collider.radius += collider.radius * deltaTime / dividedBy;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            playerIsInSafeZone = true;
            Debug.Log(playerIsInSafeZone);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            playerIsInSafeZone = false;
            Debug.Log(playerIsInSafeZone);
        }
    }
}
