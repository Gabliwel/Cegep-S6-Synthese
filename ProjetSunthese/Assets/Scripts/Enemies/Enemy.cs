using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float hp;
    [SerializeField] protected Color particleColor = new Color(255,0,0);
    [SerializeField] protected float particleScale = 1;

    public virtual void Harm(float ammount)
    {
        Debug.Log(name + " ouched for " + ammount + " damage.");
        hp -= ammount;
        if (hp <= 0)
            Die();
    }

    public virtual void Die()
    {
        Debug.Log(name + " deathed forever");
        Drop();
        gameObject.SetActive(false);
        ParticleManager.instance.CallParticles(transform.position, particleScale, particleColor);
    }

    protected abstract void Drop();
}
