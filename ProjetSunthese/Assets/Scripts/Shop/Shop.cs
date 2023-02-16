using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject[] stands;
    [SerializeField] private List<GameObject> possibleItems;

    void Awake()
    {
        for (int i = 0; i < stands.Length; i++)
        {
            int nb = Random.Range(0, possibleItems.Count);
            GameObject item = Instantiate(possibleItems[nb], stands[i].transform);
            possibleItems.RemoveAt(nb);

            item.transform.position = stands[i].transform.position + (Vector3.up * 2);

            item.GetComponent<Interactable>().enabled = false;
            item.transform.GetChild(0).gameObject.SetActive(false);
            item.AddComponent<ShopItemMovement>();

            stands[i].GetComponent<ShopItem>().SetItem(item);


            item.SetActive(true);
        }
    }
}
