using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPoint : MonoBehaviour
{
    private Animator animator;

    [Header("Link")]
    [SerializeField] private SpriteRenderer laserSprite;
    [SerializeField] private Sensor sensor;

    [Header("Stats")]
    [SerializeField] private Vector3 smallScale;
    [SerializeField] private Vector3 bigScale;
    [SerializeField] private float damage = 3;
    [SerializeField] private float rate = 0.2f;

    private Coroutine coroutine;

    private ISensor<Player> playerSensor;
    private bool isActive = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
        DeactivateSensor();
        laserSprite.enabled = false;
    }

    public void Charge(float chargeTime)
    {
        laserSprite.enabled = false;
        StartCoroutine(ScaleChanger(chargeTime, smallScale, bigScale));
    }

    private IEnumerator ScaleChanger(float chargeTime, Vector3 firstScale, Vector3 lastScale)
    {
        for (float time = 0; time < chargeTime; time += Time.deltaTime)
        {
            transform.localScale = Vector3.Lerp(firstScale, lastScale, time / chargeTime);
            yield return null;
        }
    }

    public void StartMovement(Vector3 pivotPoint)
    {
        laserSprite.enabled = false;
        isActive = true;
        StartCoroutine(Rotate(pivotPoint));
    }

    private IEnumerator Rotate(Vector3 pivotPoint)
    {
        while (isActive)
        {
            transform.RotateAround(pivotPoint, new Vector3(0, 0, 1), Time.deltaTime * 50);
            yield return null;
        }
    }

    public void ActivateLaser(float time)
    {
        isActive = true;
        StartCoroutine(ActiveLaser(time));
    }

    private IEnumerator ActiveLaser(float time)
    {
        float attackTime = time - 0.3f - 0.24f;
        laserSprite.enabled = true;
        animator.SetTrigger("On");
        yield return new WaitForSeconds(0.3f);
        ActivateSensor();
        yield return new WaitForSeconds(attackTime);
        animator.SetTrigger("Off");
        DeactivateSensor();
        yield return new WaitForSeconds(0.24f);
        laserSprite.enabled = false;

    }

    public void DeactivateLaser(float unChargeTime)
    {
        isActive = false;
        StartCoroutine(ScaleChanger(unChargeTime, bigScale, smallScale));
    }

    private void OnPlayerSense(Player player)
    {
        coroutine = StartCoroutine(Tick(player));
    }

    private void OnPlayerUnsense(Player player)
    {
        StopCoroutine(coroutine);
    }

    IEnumerator Tick(Player player)
    {
        while (isActiveAndEnabled)
        {
            player.Harm(damage);
            yield return new WaitForSeconds(rate);
        }
    }

    void ActivateSensor()
    {
        sensor.gameObject.SetActive(true);
    }
    void DeactivateSensor()
    {
        sensor.gameObject.SetActive(false);
    }
}
