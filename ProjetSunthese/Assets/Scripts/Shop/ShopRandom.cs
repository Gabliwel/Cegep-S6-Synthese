using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopRandom : MonoBehaviour
{
    [SerializeField] Vector3[] positionItem;
    [SerializeField] List<GameObject> itemList;
    void Start()
    {
        SpawnItem();
    }

    private void SpawnItem()
    {
        for(int i = 0; i < positionItem.Length; i++)
        {
            int nb = Random.Range(0, itemList.Count);
            GameObject item = Instantiate(itemList[nb]);
            itemList.RemoveAt(nb);
            item.transform.position = positionItem[i];
            item.SetActive(true);
        }
    }
}
