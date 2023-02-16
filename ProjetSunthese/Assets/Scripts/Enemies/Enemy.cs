using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float baseHP;
    [SerializeField] protected float hp;
    [SerializeField] protected Color particleColor = new Color(255, 0, 0);
    [SerializeField] protected float particleScale = 1;
    [SerializeField] protected float baseDamageDealt;
    [SerializeField] protected float damageDealt;
    [SerializeField] protected int goldDropped;
    [SerializeField] protected int xpGiven;
    protected float overtime = 0;
    protected float overtimeTimer = 1f;
    protected int scalingLevel;

    protected float poisonDuration = 5f;
    protected float poisonDamage = 0f;

    private void Update()
    {
        if (overtime > 0)
        {
            overtimeTimer -= Time.deltaTime;

            if (overtimeTimer <= 0)
            {
                overtimeTimer = 1f;
                hp -= 1;
                overtime -= 1;

                if (hp <= 0)
                {
                    Die();
                }
            }
        }
    }

    private void OnEnable()
    {
        hp = Scaling.instance.CalculateHealthOnScaling(baseHP);
        damageDealt = Scaling.instance.CalculateDamageOnScaling(baseDamageDealt);
    }

    public virtual void Harm(float ammount, float poison)
    {
        Debug.Log(name + " ouched for " + ammount + " damage.");
        hp -= ammount;

        if (poison > 0)
        {
            poisonDamage = poison;
            overtime = poisonDuration;
        }

        if (hp <= 0)
        {
            Die();
        }
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
