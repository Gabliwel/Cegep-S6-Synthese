using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProximitySensor : MonoBehaviour
{
    [SerializeField] private float minDistance;
    [SerializeField] private bool checkLayer;
    private bool isClose;
    private void FixedUpdate()
    {
        bool sameLayer = true;
        if(checkLayer)
        {
            sameLayer = gameObject.layer == Player.instance.gameObject.layer;
        }

        isClose = Vector3.Distance(transform.position, Player.instance.transform.position) <= minDistance && sameLayer;
    }

    public bool IsClose()
    {
        return isClose;
    }

    public void CheckLayer(bool check)
    {
        checkLayer = check;
    }
}
