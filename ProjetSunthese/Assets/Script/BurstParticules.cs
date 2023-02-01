using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstParticules : MonoBehaviour
{
    [SerializeField] private ParticleSystem collisionParticule;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            
        }
    }

    private void OnDisable()
    {
        Debug.Log("ON DISABLED");
        var emission = collisionParticule.emission;

        emission.enabled = true;

        collisionParticule.Play();
    }
}
