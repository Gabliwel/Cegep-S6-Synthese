using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBofrerHomingBolt : MonoBehaviour
{
    private BossBofrerHomingBoltExplosion explosion;
    private BossBofrerHomingBoltProjectile projectile;

    private void Awake()
    {
        explosion = GetComponentInChildren<BossBofrerHomingBoltExplosion>();
        projectile = GetComponentInChildren<BossBofrerHomingBoltProjectile>();
    }

    private void OnEnable()
    {
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
