using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestCollider : MonoBehaviour
{
    Tilemap chestLevel;
    void Start()
    {
        chestLevel = GameObject.FindGameObjectWithTag("Chest").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Die()
    {
        GameManager.instance.PlayerDie();
    }
}
