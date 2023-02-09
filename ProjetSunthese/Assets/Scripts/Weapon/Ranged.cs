using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Weapon
{
    [SerializeField] private int projectileNb = 20;
    [SerializeField] private Projectile projectilePrefab;
    private Projectile[] projectiles;

    private Animator animator;
    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        projectiles = new Projectile[projectileNb];
        for (int i = 0; i < projectileNb; i++)
        {
            projectiles[i] = GameObject.Instantiate<Projectile>(projectilePrefab);
            projectiles[i].gameObject.SetActive(false);
        }
    }

    protected override IEnumerator Attack()
    {
        cooldownTimer = cooldown + startup + recovery;
        animator.SetBool("Pull", true);
        yield return new WaitForSeconds(startup);
        while (Input.GetKey(KeyCode.Mouse0))
            yield return null;
        orbit = false;
        animator.SetBool("Recoil", true);
        animator.SetBool("Pull", false);
        SpawnProjectile();
        yield return new WaitForSeconds(recovery);
        animator.SetBool("Recoil", false);
        orbit = true;
    }

    private void SpawnProjectile()
    {
        Projectile projectile = GetAvailableProjectile();
        projectile.transform.SetPositionAndRotation(transform.position, transform.rotation);
        projectile.gameObject.SetActive(true);
    }

    private Projectile GetAvailableProjectile()
    {
        foreach (Projectile projectile in projectiles)
        {
            if (!projectile.gameObject.activeSelf)
                return projectile;
        }

        projectiles[projectiles.Length - 1].gameObject.SetActive(false);
        return projectiles[projectiles.Length - 1];
    }
}
