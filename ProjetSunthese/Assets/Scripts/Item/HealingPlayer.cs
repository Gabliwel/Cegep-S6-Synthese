using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPlayer : MonoBehaviour
{
    [SerializeField] int healAmount;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().Heal(healAmount);
            gameObject.SetActive(false);
        }
    }
}
