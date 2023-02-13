using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrerBall : BossAttack
{
    private BossBofrerBallExplosion explosion;
    private BossBofrerBallProjectile projectile;

    private void Awake()
    {
        explosion = GetComponentInChildren<BossBofrerBallExplosion>();
        projectile = GetComponentInChildren<BossBofrerBallProjectile>();
        explosion.gameObject.SetActive(false);
        projectile.gameObject.SetActive(false);
    }
    public override void Launch()
    {
        gameObject.SetActive(true);
        explosion.gameObject.SetActive(false);
        projectile.gameObject.SetActive(true);
        projectile.transform.position = transform.position;
    }

    public void ProjectileReached()
    {
        explosion.gameObject.SetActive(true);
        explosion.transform.position = projectile.transform.position;
    }

    public void ExplosionFinished()
    {
        gameObject.SetActive(false);
    }
}
