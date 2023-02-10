using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopRandom : MonoBehaviour
{
    [SerializeField] Vector3[] positionItem;
    [SerializeField] List<GameObject> itemListPrefab;
    void Start()
    {
        SpawnItem();
    }


    void Update()
    {
        
    }

    private void SpawnItem()
    {
        for (int i = 0; i < positionItem.Length; i++)
        {
            int itemNumber = Random.Range(0, itemListPrefab.Count);
            GameObject item = Instantiate(itemListPrefab[itemNumber]);
            item.transform.position = positionItem[i];
            item.SetActive(true);
            itemListPrefab.RemoveAt(itemNumber);
        }
    }
}
