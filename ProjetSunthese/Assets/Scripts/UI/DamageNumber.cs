using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private float ttl;
    [SerializeField] private float fadeoutAmmount;
    private float ttlTimer;
    private TextMeshProUGUI tmp;
    private float fadeTimer;
    private bool fading;

    private void Awake()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        ttlTimer = ttl;
        fading = false;
    }
    private void Update()
    {
        if (ttlTimer > 0)
            ttlTimer -= Time.deltaTime;
        else if (!fading)
        {
            fading = true;
        }
        if (fading)
        {
            fadeTimer -= Time.deltaTime;
            Color color = tmp.color;
            color.a -= Time.deltaTime * fadeoutAmmount;
            tmp.color = color;
            if (color.a <= 0)
                gameObject.SetActive(false);
        }
    }

    public void SetText(string text, Color color)
    {
        tmp.SetText(text);
        tmp.color = color;
    }
}
