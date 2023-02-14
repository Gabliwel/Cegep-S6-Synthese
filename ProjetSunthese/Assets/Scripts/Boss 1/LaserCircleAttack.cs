using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCircleAttack : BossAttack
{
    [SerializeField] private GameObject prefab;

    [Header("Base Stats")]
    [SerializeField] private int numberBullets;
    [SerializeField] private float time;
    [SerializeField] private float chargeTime;
    [SerializeField] private float unchargeTime;

    [Header("Distance")]
    [SerializeField] private float yAddedValue;
    [SerializeField] private float bulletFirstDistance;

    private GameObject[] lasers;

    void Awake()
    {
        lasers = new GameObject[numberBullets];

        for (int i = 0; i < numberBullets; i++)
        {
            lasers[i] = Instantiate(prefab, transform);
            lasers[i].SetActive(false);
        }
    }

    public override void Launch()
    {
        isAvailable = false;
        for (int i = 0; i < numberBullets; i++)
        {
            float rad = i * Mathf.PI * 2f / numberBullets;
            Vector3 newPos = transform.position + (new Vector3(Mathf.Cos(rad) * numberBullets * bulletFirstDistance, Mathf.Sin(rad) * numberBullets * bulletFirstDistance + yAddedValue, 0));
            lasers[i].SetActive(true);
            lasers[i].transform.position = newPos;
            lasers[i].transform.rotation = Quaternion.Euler(0, 0, ((180 / Mathf.PI) * rad));
            lasers[i].GetComponent<LaserPoint>().Charge(chargeTime);
        }

        StartCoroutine(StartLaserPoint());
    }

    private IEnumerator StartLaserPoint()
    {
        yield return new WaitForSeconds(chargeTime);
        for (int i = 0; i < numberBullets; i++)
        {
            lasers[i].GetComponent<LaserPoint>().StartMovement(transform.position + (Vector3.up * yAddedValue));
        }
        StartCoroutine(SimpleLaserCicle());
    }


    private IEnumerator SimpleLaserCicle()
    {
        for (int i = 0; i < numberBullets; i+=2)
        {
            lasers[i].GetComponent<LaserPoint>().ActivateLaser(time);
        }
        yield return new WaitForSeconds(time + 0.54f);
        for (int i = 1; i < numberBullets; i += 2)
        {
            lasers[i].GetComponent<LaserPoint>().ActivateLaser(time);
        }
        yield return new WaitForSeconds(time + 0.54f);
        for (int i = 0; i < numberBullets; i ++)
        {
            lasers[i].GetComponent<LaserPoint>().ActivateLaser(time);
        }
        yield return new WaitForSeconds(time);
        StartCoroutine(DeactivateLasers());
    }

    private IEnumerator DeactivateLasers()
    {
        for (int i = 0; i < numberBullets; i++)
        {
            lasers[i].GetComponent<LaserPoint>().DeactivateLaser(unchargeTime);
        }
        yield return new WaitForSeconds(unchargeTime);
        for (int i = 0; i < numberBullets; i++)
        {
            lasers[i].SetActive(false);
        }

        isAvailable = true;
    }
}
