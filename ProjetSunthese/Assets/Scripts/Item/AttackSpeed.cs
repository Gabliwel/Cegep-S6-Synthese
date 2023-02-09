using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : MonoBehaviour
{
    private GameObject player;
    private float time = 1.5f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if(time >= 0)
        {
            time -= Time.deltaTime;

            if(time <= 0)
            {
                GetComponent<Collider2D>().enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().IncreaseAttackSpeed();
            gameObject.SetActive(false);
        }
    }
}
