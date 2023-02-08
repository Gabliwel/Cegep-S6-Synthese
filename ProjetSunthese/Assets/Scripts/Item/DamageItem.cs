using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageItem : MonoBehaviour
{
    private GameObject weapon;
    void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            weapon.GetComponent<Weapon>().BoostDamage();
            gameObject.SetActive(false);
        }
    }
}
