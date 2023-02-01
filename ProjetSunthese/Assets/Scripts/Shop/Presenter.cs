using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Presenter : MonoBehaviour
{
    private GameObject item;
    private GameObject backItem;

    void Start()
    {
        item = gameObject.transform.GetChild(0).gameObject;
        backItem = item.transform.GetChild(0).gameObject;
        backItem.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        backItem.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        backItem.SetActive(false);
    }
}
