using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLavaController : Enemy
{
    protected override void Drop()
    {
        RandomDropManager.instance.SelectRandomItem();
        gameObject.SetActive(false);
        //RandomDropManager.instance.DropItem(GetComponent<Collider2D>().bounds.center, collision.gameObject.GetComponent<Collider2D>().bounds.center);
    }

    void Awake()
    {
        
    }

    void Update()
    {
        
    }

    public void Ascend()
    {
        gameObject.GetComponentInChildren<ParticleSystem>().Play();
    }
}
