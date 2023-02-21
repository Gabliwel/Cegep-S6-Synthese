using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] protected float damage;
    [SerializeField] protected float duration;
    [SerializeField] protected float timer;
    protected Sensor sensor;

    protected virtual void Awake()
    {
        sensor = GetComponentInChildren<Sensor>();

        damage = Scaling.instance.CalculateDamageOnScaling(damage);
    }

    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
            gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        timer = duration;
    }
}
