using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling : MonoBehaviour
{
    [SerializeField] int currentScaling;
    void Start()
    {
        if(currentScaling <= 0)
        {
            currentScaling = 1;
        }
    }

    void Update()
    {
        
    }

    public int SendScaling()
    {
        return currentScaling;
    }

    public void ScalingIncrease()
    {
        currentScaling++;
    }
}
