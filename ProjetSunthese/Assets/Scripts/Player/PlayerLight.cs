using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLight : MonoBehaviour
{
    [SerializeField] private string startingRenderLayer;

    [Header("URP Lights Components")]
    [SerializeField] private Light2D layer1Light;
    [SerializeField] private Light2D layer2Light;

    void Start()
    {
        UpdateLightUsage(startingRenderLayer);
    }

    public void UpdateLightUsage(string renderLayer)
    {
        if(renderLayer == "Layer 1")
        {
            layer1Light.enabled = true;
            layer2Light.enabled = false;
        }
        else if(renderLayer == "Layer 2")
        {
            layer1Light.enabled = false;
            layer2Light.enabled = true;
        }
    }
}
