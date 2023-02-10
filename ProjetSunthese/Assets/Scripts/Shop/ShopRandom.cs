using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopRandom : MonoBehaviour
{
    [SerializeField] Vector3[] positionItem;
    private List<GameObject> itemList;
    void Start()
    {
        int totalWeapon = transform.childCount;
        itemList = new List<GameObject>(totalWeapon);

        for (int i = 0; i < totalWeapon; i++)
        {
            itemList.Add(transform.GetChild(i).gameObject);
            itemList[i].SetActive(false);
        }

        SpawnItem();
    }

    void Update()
    {
        
    }

    private void SpawnItem()
    {
        for(int i = 0; i < positionItem.Length; i++)
        {
            int nb = Random.Range(0, itemList.Count);
            GameObject item = itemList[nb];
            itemList.RemoveAt(nb);
            item.transform.position = positionItem[i];
            item.SetActive(true);
        }
    }
}
