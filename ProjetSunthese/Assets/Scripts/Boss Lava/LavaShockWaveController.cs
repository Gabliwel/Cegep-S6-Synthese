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

    GameObject shockWaveMask;

    private Collider2D shockWaveCollider;
    private SpriteRenderer shockWaveSpriteRenderer;
    private Collider2D shockWaveMaskCollider;
    private SpriteMask shockWaveSpriteMask;

    private Coroutine shockWaveCoroutine;
    void Awake()
    {
        originalScale = transform.localScale;
        maskController = GetComponentInChildren<LavaShockWaveMaskController>();

        shockWaveCollider = GetComponentInChildren<Collider2D>();
        shockWaveSpriteRenderer = GetComponent<SpriteRenderer>();

        shockWaveMaskCollider = transform.Find("LavaShockWaveMask").GetComponentInChildren<Collider2D>();
        shockWaveSpriteMask = transform.Find("LavaShockWaveMask").GetComponent<SpriteMask>();

        sensor = GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;

        sensor.enabled = false;
        shockWaveCollider.enabled = false;
        shockWaveMaskCollider.enabled = false;
        type = BossAttackType.Lava;
    }

    void Update()
    {
        if (spread)
            transform.localScale += transform.localScale * Time.deltaTime / 3;
    }

    private IEnumerator StartShockWave()
    {
        ReactivateColliderAndSprite();
        livedTime = 10f;
        yield return new WaitForSeconds(livedTime);
        ShrinkBackToOriginal();
        DeactivateColliderAndSprite();
        spread = false;
        StopCoroutine(shockWaveCoroutine);
    }

    void OnPlayerSense(Player player)
    {
        if (!maskController.playerIsInSafeZone)
            player.Harm(damage);
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
        sensor.enabled = true;
        shockWaveSpriteMask.enabled = true;
        shockWaveMaskCollider.enabled = true;
        shockWaveSpriteRenderer.enabled = true;
        shockWaveCollider.enabled = true;
    }

    public override void Launch()
    {
        spread = true;
        shockWaveCoroutine = StartCoroutine(StartShockWave());
    }
}
