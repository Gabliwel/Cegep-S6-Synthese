using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private float ttl;
    private float ttlTimer;
    private TextMeshProUGUI tmp;

    private void Awake()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        ttlTimer = ttl;
    }
    private void Update()
    {
        if (ttlTimer > 0)
            ttlTimer -= Time.deltaTime;
        else
            gameObject.SetActive(false);
    }

    public void SetText(string text, Color color)
    {
        tmp.SetText(text);
        tmp.color = color;
    }
}
