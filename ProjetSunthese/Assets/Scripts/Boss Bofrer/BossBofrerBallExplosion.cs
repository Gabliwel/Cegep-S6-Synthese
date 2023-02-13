using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrerBallExplosion : Explosion
{
    private BossBofrerBall parentBall;
    protected override void Awake()
    {
        base.Awake();

        parentBall = GetComponentInParent<BossBofrerBall>();
    }
    private void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        else
        {
            gameObject.SetActive(false);
            parentBall.ExplosionFinished();
        }
    }
}
