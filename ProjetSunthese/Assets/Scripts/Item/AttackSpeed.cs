using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeed : MonoBehaviour
{
    private GameObject weapon;
    void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon");
    }


    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            weapon.GetComponent<Weapon>().IncreaseAttackSpeed();
            //gameObject.SetActive(false);
        }
    }
}
