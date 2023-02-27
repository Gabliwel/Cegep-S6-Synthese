using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHolder : MonoBehaviour
{
    private BombExplosion explosion;
    private Bomb projectile;
    private void Awake()
    {
        explosion = GetComponentInChildren<BombExplosion>();
        projectile = GetComponentInChildren<Bomb>();
        explosion.gameObject.SetActive(false);
        projectile.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        gameObject.SetActive(true);
        explosion.gameObject.SetActive(false);
        projectile.gameObject.SetActive(true);
        projectile.transform.position = transform.position;
    }

    public void SetDamage(float playerDamage, float poison)
    {
        projectile.SetDamage(playerDamage / 2, poison);
        explosion.SetDamage(playerDamage);
    }

    public void SetFuse(float timer)
    {
        projectile.SetFuse(timer);
    }

    public void SetDestination(Vector2 position)
    {
        projectile.SetDestination(position);
    }

    public void ExplosionFinished()
    {
        gameObject.SetActive(false);
    }

    public void ProjectileReached()
    {
        explosion.gameObject.SetActive(true);
        explosion.transform.position = projectile.transform.position;
    }
}
