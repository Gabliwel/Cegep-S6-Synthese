using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

// https://www.youtube.com/watch?v=CFASjEuhyf4&list=WL&index=11&t=516s&ab_channel=NattyCreations
public class LifeBar : MonoBehaviour
{
    private float currentHealth = 100;
    private float maxHealth = 100;
    private Coroutine coroutine;

    [Header("Stats")]
    [SerializeField] private float speed = 2f;

    [Header("Link")]
    [SerializeField] private Image front;
    [SerializeField] private Image back;
    [SerializeField] private TMP_Text tmp;

    public void SetDefault(PlayerHealth health)
    {
        currentHealth = health.CurrentHealth;
        maxHealth = health.CurrentMax;
        front.fillAmount = currentHealth / maxHealth;
        back.fillAmount = currentHealth / maxHealth;
        UpdateText();
    }

    public void UpdateHealth(PlayerHealth health)
    {
        UpdateText();
        if (coroutine != null) StopCoroutine(coroutine);

        bool takeDamage = false;
        if (currentHealth > health.CurrentHealth) takeDamage = true;

        currentHealth = health.CurrentHealth;
        maxHealth = health.CurrentMax;
        UpdateText();

        if(takeDamage) coroutine = StartCoroutine(UpdateBarWhenDamaged());
        else coroutine = StartCoroutine(UpdateBarWheHealed());
    }

    private IEnumerator UpdateBarWheHealed()
    {
        float fraction = currentHealth / maxHealth;

        float timer = 0;
        float currentFront = front.fillAmount;
        float currentBack = back.fillAmount;
        while (fraction > front.fillAmount)
        {
            timer += Time.deltaTime;
            float percent = timer * speed;
            front.fillAmount = Mathf.Lerp(currentFront, fraction, percent);
            back.fillAmount = Mathf.Lerp(currentBack, fraction, percent);
            yield return null;
        }
    }

    private IEnumerator UpdateBarWhenDamaged()
    {
        float fraction = currentHealth / maxHealth;
        front.fillAmount = fraction;

        float timer = 0;
        float currentBack = back.fillAmount;
        while(currentBack > front.fillAmount)
        {
            timer += Time.deltaTime;
            float percent = timer * speed;
            back.fillAmount = Mathf.Lerp(currentBack, fraction, percent);
            yield return null;
        }
    }

    private void UpdateText()
    {
        tmp.text = currentHealth.ToString() + " / " + maxHealth.ToString();
    }
}
