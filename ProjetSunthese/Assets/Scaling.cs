using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaling : MonoBehaviour
{
    [SerializeField] int currentScaling;
    void Start()
    {
        
    }

    // Update is called once per frame
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
