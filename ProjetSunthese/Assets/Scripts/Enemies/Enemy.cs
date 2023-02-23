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
    [SerializeField] protected float overtime = 0;
    protected float overtimeTimer = 1f;
    protected int scalingLevel;

    protected float poisonDuration = 5f;
    protected float playerPoisonDamage = 0f;
    protected float currentPoison = 0f;
    protected bool poisonCoroutine = false;

    /// <summary>
    /// TODO: FIXME: this is bad; no time for fix in alpha
    /// </summary>

    private void OnEnable()
    {
        hp = Scaling.instance.CalculateHealthOnScaling(baseHP);
        damageDealt = Scaling.instance.CalculateDamageOnScaling(baseDamageDealt);
    }

    public virtual void Harm(float ammount, float poison)
    {
        Debug.Log(name + " ouched for " + ammount + " damage.");
        ReduceHp(ammount);

        if (poison > 0)
        {
            playerPoisonDamage = poison;
            currentPoison += poison;

            if (!poisonCoroutine && gameObject.activeSelf)
            {
                poisonCoroutine = true;
                StartCoroutine(DealPoisonDamage());
            }
        }
    }

    public void ReduceHp(float hpLost)
    {
        hp -= hpLost;

        if (hp <= 0)
        {
            Die();
        }
    }

    private IEnumerator DealPoisonDamage()
    {
        while (currentPoison > 0)
        {
            yield return new WaitForSeconds(3f);

            float damage = playerPoisonDamage;

            currentPoison -= damage;

            ReduceHp(damage);
            WasPoisonHurt();
        }
        poisonCoroutine = false;
    }

    public virtual void Die()
    {
        StopCoroutine(DealPoisonDamage());
        Drop();
        gameObject.SetActive(false);
        ParticleManager.instance.CallParticles(transform.position, particleScale, particleColor);
        AchivementManager.instance.KilledEnnemies();
    }

    protected abstract void Drop();

    /// <summary>
    /// TODO: FIXME: this is bad; no time for fix in alpha
    /// </summary>
    protected virtual void WasPoisonHurt() { }
}
