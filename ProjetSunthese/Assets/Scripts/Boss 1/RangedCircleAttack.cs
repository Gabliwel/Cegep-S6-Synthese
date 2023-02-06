using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedCircleAttack : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    [Header("Base Stats")]
    [SerializeField] private int numberBullets;
    [SerializeField] private float bulletImpulseSpeed;
    [SerializeField] private float time;

    [Header("Distance")]
    [SerializeField] private float yAddedValue;
    [SerializeField] private float bulletFirstDistance;
    [SerializeField] private float bulletLastDistance;

    private GameObject[] fireballs;

    void Start()
    {
        fireballs = new GameObject[numberBullets];

        for (int i = 0; i < numberBullets; i++)
        {
            fireballs[i] = Instantiate(prefab);
            fireballs[i].SetActive(false);
        }
    }

    public void Launch()
    {
        for (int i = 0; i < numberBullets; i++)
        {
            float rad = i * Mathf.PI * 2f / numberBullets;
            Vector3 newPos = transform.position + (new Vector3(Mathf.Cos(rad) * numberBullets * bulletFirstDistance, Mathf.Sin(rad) * numberBullets * bulletFirstDistance + yAddedValue, 0));
            fireballs[i].SetActive(true);
            fireballs[i].transform.position = newPos;
            fireballs[i].transform.rotation = Quaternion.Euler(0, 0, ((180 / Mathf.PI) * rad));
            fireballs[i].GetComponent<LaserPoint>().StartMovement(transform.position + (Vector3.up * yAddedValue));
        }   
    }
}
