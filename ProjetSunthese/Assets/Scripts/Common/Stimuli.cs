using System;
using UnityEngine;


public sealed class Stimuli : MonoBehaviour
{
    public event StimuliEventHandler OnDestroyed;
    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
    private void OnDestroy()
    {
        NotifyDestroyed();
    }

    private void NotifyDestroyed()
    {
        if (OnDestroyed != null) OnDestroyed(transform.parent.gameObject);
    }
}

public delegate void StimuliEventHandler(GameObject otherObject);
