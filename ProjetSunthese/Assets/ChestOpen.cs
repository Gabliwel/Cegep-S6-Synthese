using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChestOpen : MonoBehaviour
{
    [SerializeField] Tile coffreOuvert;
    private Tilemap tilemap;
    private bool chestClosed = true;
    void Start()
    {
        tilemap = GameObject.FindGameObjectWithTag("Chest").GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (chestClosed)
        {
            tilemap.SetTile(tilemap.WorldToCell(transform.position), coffreOuvert);
            chestClosed = false;
            Debug.Log("Give item");
        }
    }
}
