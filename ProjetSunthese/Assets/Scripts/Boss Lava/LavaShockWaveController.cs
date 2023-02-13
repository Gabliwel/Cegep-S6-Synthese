using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaShockWaveController : BossAttack
{
    Vector3 originalScale;
    LavaShockWaveMaskController maskController;
    private Sensor sensor;
    private ISensor<Player> playerSensor;
    [SerializeField] private float damage;
    private float timeToLive = 5;
    private float livedTime = 0;

    private bool spread = false;

    private Collider2D shockWaveCollider;
    private SpriteRenderer shockWaveSpriteRenderer;
    private Collider2D shockWaveMaskCollider;
    private SpriteMask shockWaveSpriteMask;

    void Awake()
    {
        originalScale = transform.localScale;
        maskController = GetComponentInChildren<LavaShockWaveMaskController>();

        shockWaveCollider = GetComponent<Collider2D>();
        shockWaveSpriteRenderer = GetComponent<SpriteRenderer>();
        shockWaveMaskCollider = GetComponentInChildren<Collider2D>();
        shockWaveSpriteMask = GetComponentInChildren<SpriteMask>();

        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;

    }

    void Update()
    {
        if (spread)
        {
            ReactivateColliderAndSprite();
            transform.localScale += transform.localScale * Time.deltaTime / 3;
            livedTime += Time.deltaTime;
            if (livedTime > timeToLive)
            {
                spread = false;
                livedTime = 0;
                ShrinkBackToOriginal();
                DeactivateColliderAndSprite();
            }
        }
    }



    void OnPlayerSense(Player player)
    {
        if (!maskController.playerIsInSafeZone)
        {
            player.Harm(damage);
        }
    }


    void OnPlayerUnsense(Player player)
    {
    }

    private void OnDisable()
    {
        ShrinkBackToOriginal();
    }

    private void ShrinkBackToOriginal()
    {
        DeactivateColliderAndSprite();
        transform.localScale = originalScale;
    }

    private void DeactivateColliderAndSprite()
    {
        shockWaveSpriteMask.enabled = false;
        shockWaveMaskCollider.enabled = false;
        shockWaveSpriteRenderer.enabled = false;
        shockWaveCollider.enabled = false;
    }

    private void ReactivateColliderAndSprite()
    {
        shockWaveSpriteMask.enabled = true;
        shockWaveMaskCollider.enabled = true;
        shockWaveSpriteRenderer.enabled = true;
        shockWaveCollider.enabled = true;
    }

    public override void Launch()
    {
        spread = true;
    }
}
