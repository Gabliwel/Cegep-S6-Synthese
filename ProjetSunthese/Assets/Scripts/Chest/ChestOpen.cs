using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChestOpen : MonoBehaviour
{
    [SerializeField] Tile coffreOuvert;
    private Tilemap tilemap;
    private bool chestClosed = true;
    private ChestDropManager chestDropManager;

    private AchivementManager achivement;
    void Start()
    {
        chestDropManager = GameObject.FindGameObjectWithTag("Chest").GetComponent<ChestDropManager>();
        tilemap = GameObject.FindGameObjectWithTag("Chest").GetComponent<Tilemap>();
        achivement = GameObject.FindGameObjectWithTag("Achivement").GetComponent<AchivementManager>();
    }

    void Update()
    {

    }

    [ContextMenu("test")]
    public void Test()
    {
        Debug.Log("test");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (chestClosed)
        //{
        //    tilemap.SetTile(tilemap.WorldToCell(transform.position), coffreOuvert);
        //    chestClosed = false;
        //    Debug.Log("Give item");
        //    GameObject drop = chestDropManager.SendRandomItem();
        //    drop.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0);
        //    drop.SetActive(true);
        //    achivement.OpenedChest();
        //}
    }
}
