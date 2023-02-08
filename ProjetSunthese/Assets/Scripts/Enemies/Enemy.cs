using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected float hp;
    protected float overtime = 0;
    protected float overtimeTimer = 1f;
    protected Scaling scaling;
    protected int xpGiven;

    private void Update()
    {
        if(overtime > 0)
        {
            overtimeTimer -= Time.deltaTime;

            if(overtimeTimer <= 0)
            {
                overtimeTimer = 1f;
                hp -= 1;
                overtime -= 1;

                if(hp <= 0)
                {
                    Die();
                }
            }
        }
    }

    public virtual void Harm(float ammount, float overtimeDamage)
    {
        Debug.Log(name + " ouched for " + ammount + " damage.");
        hp -= ammount;
        overtime += overtimeDamage;

        if(hp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Drop();
        gameObject.SetActive(false);
    }

    protected abstract void Drop();
}
