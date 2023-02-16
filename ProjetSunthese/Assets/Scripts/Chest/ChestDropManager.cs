using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestDropManager : MonoBehaviour
{
    [SerializeField] GameObject[] itemDrop;
    private GameObject[] availableDrop;
    void Awake()
    {
        availableDrop = new GameObject[itemDrop.Length * 5];
        for (int i = 0; i < itemDrop.Length; i++)
        {
            // Instantiate each possible drop 5 times
            availableDrop[(i * 5)] = Instantiate(itemDrop[i]);
            availableDrop[(i * 5)].SetActive(false);
            availableDrop[1 + (i * 5)] = Instantiate(itemDrop[i]);
            availableDrop[1 +(i * 5)].SetActive(false);
            availableDrop[2 + (i * 5)] = Instantiate(itemDrop[i]);
            availableDrop[2 + (i * 5)].SetActive(false);
            availableDrop[3 + (i * 5)] = Instantiate(itemDrop[i]);
            availableDrop[3 + (i * 5)].SetActive(false);
            availableDrop[4 + (i * 5)] = Instantiate(itemDrop[i]);
            availableDrop[4 + (i * 5)].SetActive(false);
        }

        foreach (GameObject g in availableDrop)
        {
            g.SetActive(false);
        }

    }

    public GameObject SendRandomItem()
    {
        int safeStateMax = 0;
        while (true)
        {
            int itemNB = Random.Range(0, availableDrop.Length);
            if (!availableDrop[itemNB].activeSelf)
            {
                return availableDrop[itemNB];
            }
            safeStateMax++;

            if(safeStateMax >= availableDrop.Length -1)
            {
                return Instantiate(itemDrop[Random.Range(0, itemDrop.Length)]);
            }
        }
    }
}
