using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyExplosion : Explosion
{
    private ExplosiveEnnemyHolder parentHolder;

    protected override void Awake()
    {
        base.Awake();

        parentHolder = GetComponentInParent<ExplosiveEnnemyHolder>();
    }
    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            gameObject.SetActive(false);
        }
    }
}
