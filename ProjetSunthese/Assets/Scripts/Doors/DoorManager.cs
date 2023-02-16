using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private Door[] doors;
    [SerializeField] private Vector3 caveLocation;
    [SerializeField] private string caveLayerName;
    private Vector3 returnLocation;
    private string returnLayer;
    void Start()
    {
        if (doors.Length > 0)
        {
            int i = Random.Range(0, doors.Length - 1);
            doors[i].ActivateDoor();
        }
        returnLocation = caveLocation;
    }

    public void PlayerWentThrough(Vector3 location)
    {
        Player.instance.transform.position = returnLocation;
        returnLocation = location;
        Player.instance.ChangeLayer(returnLayer, returnLayer);
    }

    public void PlayerWentThrough(Vector3 location, string layer)
    {
        Player.instance.transform.position = returnLocation;
        Player.instance.ChangeLayer(caveLayerName, caveLayerName);
        returnLocation = location;
        returnLayer = layer;
    }
}
