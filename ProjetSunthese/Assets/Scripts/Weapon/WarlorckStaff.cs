using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarlorckStaff : Weapon
{
    private SpriteRenderer sprite;
    protected WarlockProjectileHolder[] projectiles = null;

    protected Animator animator;

    protected override void Update()
    {
        base.Update();
        ManageFlip();
    }

    void ManageFlip()
    {
        if (orbit)
        {
            float axis = Mathf.Abs(Mathf.Atan2(mouseRelativeToPlayer.y, mouseRelativeToPlayer.x) * Mathf.Rad2Deg);
            sprite.flipY = axis > 90;

        }
    }
    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    protected override IEnumerator Attack()
    {
        if (projectiles == null) yield break;
        cooldownTimer = cooldown + startup + recovery;
        animator.SetBool("Charge", true);

        yield return new WaitForSeconds(startup);
        while (Input.GetKey(KeyCode.Mouse0))
            yield return null;
        orbit = false;
        animator.SetBool("Charge", false);
        SpawnProjectile();
        yield return new WaitForSeconds(recovery);
        orbit = true;
    }

    protected override void EndAttackSpecifics()
    {
        animator.SetBool("Pull", false);
        animator.Play("warlock-staff-idle");
        cooldownTimer = 0;

        foreach (WarlockProjectileHolder projectile in projectiles)
        {
            projectile.gameObject.SetActive(false);
        }
    }

    private void SpawnProjectile()
    {
        WarlockProjectileHolder projectile = GetAvailableProjectile();
        projectile.transform.SetPositionAndRotation(transform.position, transform.rotation);
        projectile.gameObject.SetActive(true);
        projectile.SetDamage(baseDamage * playerBaseWeaponStat.GetBaseAttackMultiplicator(), playerBaseWeaponStat.GetPoisonLevel());
        projectile.SetDestination(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    private WarlockProjectileHolder GetAvailableProjectile()
    {
        foreach (WarlockProjectileHolder projectile in projectiles)
        {
            if (!projectile.gameObject.activeSelf)
                return projectile;
        }

        projectiles[projectiles.Length - 1].gameObject.SetActive(false);
        return projectiles[projectiles.Length - 1];
    }

    public void SetProjectiles(WarlockProjectileHolder[] newProjectiles)
    {
        projectiles = newProjectiles;
    }
}
