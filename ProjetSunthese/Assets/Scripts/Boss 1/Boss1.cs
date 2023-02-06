using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    private GrowingAttackZone growingZoneAttack;
    private RangedCircleAttack rangedCircleAttack;


    void Start()
    {
        growingZoneAttack = gameObject.GetComponentInChildren<GrowingAttackZone>();
        rangedCircleAttack = gameObject.GetComponentInChildren<RangedCircleAttack>();
    }

    void Update()
    {
        // Pour tests
        if(Input.GetKeyDown(KeyCode.O))
        {
            growingZoneAttack.Launch();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            rangedCircleAttack.Launch();
        }
    }
}
