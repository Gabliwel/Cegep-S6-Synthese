using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LavaPoint : MonoBehaviour
{
    [Header("Link")]
    [SerializeField] private GameObject lavaGrid;
    [SerializeField] private GameObject lavaBall;
    [SerializeField] private Sensor sensor;

    [Header("Stats")]
    [SerializeField] private float activeTime;
    [SerializeField] private float fadeOutSpeed;
    [SerializeField] private float damage = 2;
    [SerializeField] private float rate = 0.25f;

    private Tilemap lavaGridTilemap;
    private ISensor<Player> playerSensor;

    private Coroutine coroutine;

    private void Awake()
    {
        lavaGridTilemap = lavaGrid.GetComponentInChildren<Tilemap>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
        DeactivateSensor();
    }

    public void Launch(Vector2 objective)
    {
        lavaGridTilemap.color = Color.white;
        lavaGrid.transform.rotation = Quaternion.Euler(1, 1, 1);

        lavaGrid.SetActive(false);
        lavaBall.SetActive(true);
        StartCoroutine(BallToPosition(objective));
    }

    private IEnumerator BallToPosition(Vector2 objective)
    {
        while (Vector2.Distance(transform.position, objective) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, objective, 1.5f * Time.deltaTime);
            yield return null;
        }

        StartCoroutine(RotateToEndMoveTowards(objective));
    }

    private IEnumerator RotateToEndMoveTowards(Vector2 objective)
    {
        float rotationSense;
        if (UnityEngine.Random.value < 0.5f)
            rotationSense = 1;
        else
            rotationSense = -1;

        lavaGrid.SetActive(true);
        lavaBall.SetActive(false);
        lavaGrid.transform.localScale = Vector3.zero;

        while (lavaGrid.transform.localScale.x < 0.7)
        {
            lavaGrid.transform.localScale = new Vector3(lavaGrid.transform.localScale.x + (1.5f * Time.deltaTime), lavaGrid.transform.localScale.y + (1.5f * Time.deltaTime), 1);
            lavaGrid.transform.Rotate(Vector3.forward * (100 * Time.deltaTime) * rotationSense);
            yield return null;
        }
        StartCoroutine(WaitAndFadeOut());
    }

    private IEnumerator WaitAndFadeOut()
    {
        ActivateSensor();
        yield return new WaitForSeconds(activeTime);
        DeactivateSensor();

        while (lavaGridTilemap.color.a > 0.1f)
        {
            lavaGridTilemap.color = new Color(lavaGridTilemap.color.r, lavaGridTilemap.color.b, lavaGridTilemap.color.g, lavaGridTilemap.color.a - fadeOutSpeed * Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
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
