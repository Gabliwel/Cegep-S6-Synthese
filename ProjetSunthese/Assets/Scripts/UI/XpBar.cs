using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class XpBar : MonoBehaviour
{
    [Header("Link")]
    [SerializeField] private Image front;
    [SerializeField] private TMP_Text tmp;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) UpdateBar(front.fillAmount * 100 + 2, 100, 1);
        if (Input.GetKeyDown(KeyCode.K)) UpdateBar(50, 100, 1);
        if (Input.GetKeyDown(KeyCode.J)) UpdateBar(70, 100, 2);
    }

    public void UpdateBar(float xp, float max, int lvl)
    {
        front.fillAmount = xp / max;
        tmp.text = "LVL " + lvl.ToString();
    }
}
