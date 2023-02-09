using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    private GrowingAttackZone growingZoneAttack;
    private LaserCircleAttack rangedCircleAttack;
    private LavaThrowAttack lavaThrowAttack;


    void Start()
    {
        growingZoneAttack = gameObject.GetComponentInChildren<GrowingAttackZone>();
        rangedCircleAttack = gameObject.GetComponentInChildren<LaserCircleAttack>();
        lavaThrowAttack = gameObject.GetComponentInChildren<LavaThrowAttack>();
    }

    void Update()
    {
        // Pour tests
        if(Input.GetKeyDown(KeyCode.O) && growingZoneAttack.IsUsable())
        {
            growingZoneAttack.Launch();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            rangedCircleAttack.Launch();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            lavaThrowAttack.Launch();
        }
    }
}
