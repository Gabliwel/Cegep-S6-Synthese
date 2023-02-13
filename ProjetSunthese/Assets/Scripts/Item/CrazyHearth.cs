using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyHearth : GeneralItem
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<Player>().GetDoubleNumber();
            gameObject.SetActive(false);
        }
    }
}
