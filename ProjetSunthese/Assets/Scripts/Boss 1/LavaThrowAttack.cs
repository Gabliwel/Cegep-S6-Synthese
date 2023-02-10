using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaThrowAttack : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    [Header("Base Stats")]
    [SerializeField] private int numberLavaObjects;
    [SerializeField] private int minToThrow;
    [SerializeField] private int maxToThrow;

    [Header("Distance")]
    [SerializeField] private Transform minDistance;
    [SerializeField] private Transform maxDistance;

    private GameObject[] lavaObjects;

    void Start()
    {
        lavaObjects = new GameObject[numberLavaObjects];

        for (int i = 0; i < numberLavaObjects; i++)
        {
            lavaObjects[i] = Instantiate(prefab, transform);
            lavaObjects[i].SetActive(false);
        }
    }

    public void Launch()
    {
        // max is exclusive
        int rand = Random.Range(minToThrow, maxToThrow + 1);
        int count = 0;

        foreach(GameObject lava in lavaObjects)
        {
            if(!lava.activeSelf)
            {
                lava.SetActive(true);
                lava.gameObject.transform.position = transform.position;
                lava.gameObject.GetComponent<LavaPoint>().Launch(
                    new Vector2(
                    Random.Range(minDistance.position.x, maxDistance.position.x),
                    Random.Range(minDistance.position.y, maxDistance.position.y)));
                count++;
                if (count >= rand) break;
            }
        }
    }
}
