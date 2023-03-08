using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private Color harmColor = new Color(255, 0, 0, 100);
    private Color poisonColor = new Color(0, 255, 0, 100);
    [SerializeField] private bool spriteInFirstChild = false;
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
    private SpriteRenderer sprite;
    private float flashTime = 0.1f;
    private bool flashing = false;
    protected float scaledHp;

    protected virtual void Awake()
    {
        scaledHp = baseHP;
        if (spriteInFirstChild)
        {
            sprite = transform.GetChild(0).GetComponent<SpriteRenderer>();
        }
        else
        {
            sprite = GetComponent<SpriteRenderer>();
        }
    }

    protected virtual void OnEnable()
    {
        scaledHp = Scaling.instance.CalculateHealthOnScaling(baseHP);
        hp = scaledHp;
        damageDealt = Scaling.instance.CalculateDamageOnScaling(baseDamageDealt);
        if (sprite != null)
            sprite.color = Color.white;
        flashing = false;
        poisonCoroutine = false;
    }

    public virtual void Harm(float ammount, float poison)
    {
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
        if (!flashing)
            StartCoroutine(HarmFlash());
        hp -= hpLost;
        DamageNumbersManager.instance.CallText(hpLost, transform.position, false);

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

    private IEnumerator HarmFlash()
    {
        flashing = true;
        sprite.color = harmColor;
        yield return new WaitForSeconds(flashTime);
        sprite.color = Color.white;
        flashing = false;
    }

    public virtual void Die()
    {
        StopCoroutine(DealPoisonDamage());
        Drop();
        gameObject.SetActive(false);
        ParticleManager.instance.CallParticles(transform.position, particleScale, particleColor);
        AchivementManager.instance.KilledEnnemies();
        SoundMaker.instance.EnemyDeathSound(gameObject.transform.position);
    }

    public virtual void ChangeLayer(int layer)
    {
        gameObject.layer = layer;
        sprite.sortingLayerName = LayerMask.LayerToName(layer);
        foreach (Transform child in transform)
        {
            child.gameObject.layer = layer;
        }
    }

    protected abstract void Drop();

    /// <summary>
    /// TODO: FIXME: this is bad; no time for fix in alpha
    /// </summary>
    protected virtual void WasPoisonHurt() { }
}
