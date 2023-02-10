using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            RandomDropManager.instance.SelectRandomItem();
            gameObject.SetActive(false);
            RandomDropManager.instance.DropItem(GetComponent<Collider2D>().bounds.center, collision.gameObject.GetComponent<Collider2D>().bounds.center);
        }
    }

    
}
