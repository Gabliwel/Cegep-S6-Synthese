using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemMovement : MonoBehaviour
{
    private const float ACCELERATION = 15;
    private bool sold = false;

    private void Update()
    {
        if(!sold)
        {
            Vector3 pos = gameObject.transform.position;
            float newY = Mathf.Sin(Time.time * 2) - 1.5f;
            transform.position = new Vector2(pos.x, newY * 0.2f);
        }
    }

    public void IsNowSold(SpriteRenderer sprite)
    {
        sold = true;
        StartCoroutine(IsNowSoldMove(sprite));
    }

    private IEnumerator IsNowSoldMove(SpriteRenderer sprite)
    {
        float speed = 7;
        for (float time = 0; time < 1; time += Time.deltaTime)
        {
            speed -= Time.deltaTime * ACCELERATION;
            transform.Translate(Vector2.up * speed * Time.deltaTime, Space.World);
            yield return null;
        }
        sprite.sortingOrder = 3;
        gameObject.GetComponent<Interactable>().enabled = true;
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Layer 1"; //Layer du shop
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        Destroy(this);
    }
}
