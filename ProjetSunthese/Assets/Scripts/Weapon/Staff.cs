using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : Weapon
{
    private SpriteRenderer sprite;
    protected WizardProjectile[] projectiles = null;

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
        SoundMaker.instance.PlayerStaffAttackSound(gameObject.transform.position);
        yield return new WaitForSeconds(recovery);
        orbit = true;
    }

    protected override void EndAttackSpecifics()
    {
        animator.SetBool("Charge", false);
        animator.Play("wizard-staff-idle");
        cooldownTimer = 0;

        foreach (WizardProjectile projectile in projectiles)
        {
            projectile.gameObject.SetActive(false);
        }
    }

    private void SpawnProjectile()
    {
        WizardProjectile projectile = GetAvailableProjectile();
        projectile.transform.SetPositionAndRotation(transform.position, transform.rotation);
        projectile.gameObject.SetActive(true);
        projectile.SetDamage(baseDamage * playerBaseWeaponStat.GetBaseAttackMultiplicator(), playerBaseWeaponStat.GetPoisonLevel());
    }

    private WizardProjectile GetAvailableProjectile()
    {
        foreach (WizardProjectile projectile in projectiles)
        {
            if (!projectile.gameObject.activeSelf)
                return projectile;
        }

        projectiles[projectiles.Length - 1].gameObject.SetActive(false);
        return projectiles[projectiles.Length - 1];
    }

    public void SetProjectiles(WizardProjectile[] newProjectiles)
    {
        projectiles = newProjectiles;
    }
}
