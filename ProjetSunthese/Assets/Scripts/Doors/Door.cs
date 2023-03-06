using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool isCaveExit;
    private GameObject wall;
    private GameObject door;
    private Sensor sensor;
    private ISensor<Player> playerSensor;
    private DoorManager doorManager;
    private Vector3 returnOffset = new Vector3(0, 2, 0);
    void Awake()
    {
        //à changer au besoin, ou pour être plus clean
        wall = gameObject.transform.GetChild(0).gameObject;
        door = gameObject.transform.GetChild(1).gameObject;

        if (!isCaveExit)
        {
            wall.SetActive(true);
            door.SetActive(false);
        }

        sensor = door.GetComponentInChildren<Sensor>();
        playerSensor = sensor.For<Player>();
        playerSensor.OnSensedObject += OnPlayerSense;
        playerSensor.OnUnsensedObject += OnPlayerUnsense;

        doorManager = GetComponentInParent<DoorManager>();
    }

    public void ActivateDoor()
    {
        wall.SetActive(false);
        door.SetActive(true);
    }

    private void OnPlayerSense(Player player)
    {
        if (!isCaveExit)
        {
            doorManager.PlayerWentThrough(sensor.transform.position - returnOffset, LayerMask.LayerToName(sensor.gameObject.layer));
        }
        else
        {
            doorManager.PlayerWentThrough(sensor.transform.position + returnOffset);
        }
    }

    private void OnPlayerUnsense(Player player)
    {

    }
}
