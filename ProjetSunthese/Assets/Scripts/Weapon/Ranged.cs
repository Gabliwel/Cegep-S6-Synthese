using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Weapon
{
    private Projectile[] projectiles = null;

    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override IEnumerator Attack()
    {
        if (projectiles == null) yield break;
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

    protected override void EndAttackSpecifics()
    {
        animator.SetBool("Pull", false);
        animator.SetBool("Recoil", false);
        animator.Play("bow-idle");
        cooldownTimer = 0;

        foreach (Projectile projectile in projectiles)
        {
            projectile.gameObject.SetActive(false);
        }
    }

    private void SpawnProjectile()
    {
        Projectile projectile = GetAvailableProjectile();
        projectile.transform.SetPositionAndRotation(transform.position, transform.rotation);
        projectile.gameObject.SetActive(true);

        projectile.SetDamage(baseDamage * playerBaseWeaponStat.GetBaseAttackMultiplicator(), playerBaseWeaponStat.GetPoisonLevel());
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

    public void SetProjectiles(Projectile[] newProjectiles)
    {
        projectiles = newProjectiles;
    }
}
