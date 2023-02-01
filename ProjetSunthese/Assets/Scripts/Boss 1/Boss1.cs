using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    private GrowingAttackZone growingZoneAttack;

    void Start()
    {
        growingZoneAttack = gameObject.transform.GetChild(0).gameObject.GetComponent<GrowingAttackZone>();
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.O))
        {
            growingZoneAttack.Launch();
        }
    }
}
