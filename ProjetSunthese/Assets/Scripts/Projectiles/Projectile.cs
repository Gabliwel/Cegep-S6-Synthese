using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float ttl = 10;
    [SerializeField] protected float poisonDamage;
    protected float ttlTimer;
    [SerializeField] protected Vector2 destination;

    protected virtual void Update()
    {
        if (HasDestination())
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            if (HasArrived())
            {
                DestinationReached();
            }
        }
        else
            transform.position += speed * Time.deltaTime * transform.right;
        if (ttlTimer > 0)
            ttlTimer -= Time.deltaTime;
        else
            DestinationReached();
    }

    private void OnEnable()
    {
        ttlTimer = ttl;
    }

    bool HasArrived()
    {
        return (Vector2)transform.position == destination;
    }

    protected virtual void DestinationReached()
    {
        gameObject.SetActive(false);
    }

    public void SetDestination(Vector2 position)
    {
        destination = position;
    }

    private bool HasDestination()
    {
        return destination != null && destination != Vector2.zero;
    }

    public void SetDamage(float playerDamage, float poison)
    {
        damage = playerDamage;
        poisonDamage = poison;
    }
}
