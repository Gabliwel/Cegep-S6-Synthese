using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestGameObject : Interactable
{
    private ChestDropManager chestDropManager;
    private Sensor sensor;
    private ISensor<Player> playerSensor;
    private Player player;
    private Sprite openChest;
    private bool hasBeenOpened = true;
    void Start()
    {
        chestDropManager = GameObject.FindGameObjectWithTag("Chest").GetComponent<ChestDropManager>();
        sensor = GetComponentInChildren<Sensor>(true);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;
    }

    public override void Interact(Player player) 
    {
        if (hasBeenOpened)
        {
            Debug.Log("Give item");
            GameObject drop = chestDropManager.SendRandomItem();
            drop.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0);
            drop.GetComponent<Collider2D>().enabled = false;
            drop.SetActive(true);
            hasBeenOpened = false;
        }
    }

    void OnPlayerSense(Player player)
    {
        ChangeSelectedState(true);
    }

    void OnPlayerUnsense(Player player)
    {
        ChangeSelectedState(false);
    }
}
