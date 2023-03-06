using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaController : MonoBehaviour
{
    [SerializeField] private float speedReducer = 0.75f;
    [SerializeField] private float damage = 1;
    [SerializeField] private float interval = 1;
    private bool isInLava = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        Player player = collision.gameObject.GetComponent<Player>();
        player.IsInLava(speedReducer);
        isInLava = true;
        StartCoroutine(HarmWithInterval(player));
    }

    private IEnumerator HarmWithInterval(Player player)
    {
        while(isInLava)
        {
            yield return new WaitForSeconds(interval);
            if (isInLava) player.HarmIgnoreIFrame(damage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        isInLava = false;
        collision.gameObject.GetComponent<Player>().IsNotInLava();
    }
}
