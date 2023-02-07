using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private Door[] doors;
    void Start()
    {
        if(doors.Length > 0)
        {
            int i = Random.Range(0, doors.Length -1);
            doors[i].ActivateDoor();
        }
    }
}
