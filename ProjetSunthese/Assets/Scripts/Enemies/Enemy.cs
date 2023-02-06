using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float hp;
    public virtual void Harm(float ammount)
    {
        Debug.Log(name + " ouched for " + ammount + " damage.");
    }

    public virtual void Die()
    {
        Drop();
        gameObject.SetActive(false);
        ParticleManager.instance.CallParticles(transform.position, 1);
    }

    protected abstract void Drop();
}
