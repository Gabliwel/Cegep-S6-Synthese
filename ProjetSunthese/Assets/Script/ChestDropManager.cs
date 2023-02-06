using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestDropManager : MonoBehaviour
{
    [SerializeField] GameObject[] itemDrop;
    private GameObject[] availableDrop;
    void Start()
    {
        availableDrop = new GameObject[itemDrop.Length * 5];
        for (int i = 0; i < itemDrop.Length; i++)
        {
            availableDrop[(i * 5)] = Instantiate(itemDrop[i]);
            availableDrop[1 + (i * 5)] = Instantiate(itemDrop[i]);
            availableDrop[2 + (i * 5)] = Instantiate(itemDrop[i]);
            availableDrop[3 + (i * 5)] = Instantiate(itemDrop[i]);
            availableDrop[4 + (i * 5)] = Instantiate(itemDrop[i]);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

            if(safeStateMax >= 25)
            {
                return Instantiate(itemDrop[Random.Range(0, itemDrop.Length)]);
            }
        }
    }
}
