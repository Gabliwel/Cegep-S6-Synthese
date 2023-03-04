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

    public void UpdateBar(float xp, float max, int lvl)
    {
        front.fillAmount = xp / max;
        tmp.text = "LVL " + lvl.ToString();
    }
}
