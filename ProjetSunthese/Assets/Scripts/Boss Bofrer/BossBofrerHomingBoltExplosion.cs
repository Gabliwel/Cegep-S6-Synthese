using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrerHomingBoltExplosion : Explosion
{
    private BossBofrerHomingBolt parentBolt;

    protected override void Awake()
    {
        base.Awake();

        parentBolt = GetComponentInParent<BossBofrerHomingBolt>();
    }
    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            gameObject.SetActive(false);
            parentBolt.ExplosionFinished();
        }
    }
}
