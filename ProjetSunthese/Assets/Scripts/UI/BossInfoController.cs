using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BossInfoController : MonoBehaviour
{

    private LifeBar lifeBar;
    [SerializeField] private TMP_Text name;
    public LifeBar Bar { get => lifeBar; }

    void Awake()
    {
        lifeBar = GetComponent<LifeBar>();
    }

    public void SetName(string name)
    {
        this.name.text = name;
    }

    public void Stop()
    {
        lifeBar.StopAllCoroutines();
    }
}
