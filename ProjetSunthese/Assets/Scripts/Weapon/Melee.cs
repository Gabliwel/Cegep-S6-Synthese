using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : Weapon
{
    [Header("Melee")]
    [SerializeField] private float duration;
    [Range(0, 45)]
    [SerializeField] private float windUpDistance;
    [Range(0, 45)]
    [SerializeField] private float recoilDistance;
    protected bool flipped;
    protected Sensor sensor;
    protected ISensor<Enemy> enemySensor;

    protected override void Start()
    {
        base.Start();
        sensor = GetComponentInChildren<Sensor>();
        enemySensor = sensor.For<Enemy>();
        enemySensor.OnSensedObject += OnEnemySense;
        enemySensor.OnUnsensedObject += OnEnemyUnsense;
        DeactivateSensor();
    }

    private void OnEnemySense(Enemy enemy)
    {
        enemy.Harm(baseDamage * playerBaseWeaponStat.GetBaseAttackMultiplicator(), playerBaseWeaponStat.GetPoisonLevel());
    }

    private void OnEnemyUnsense(Enemy enemy)
    {
    }

    protected void ActivateSensor()
    {
        sensor.gameObject.SetActive(true);
    }
    protected void DeactivateSensor()
    {
        sensor.gameObject.SetActive(false);
    }

    public override void StartAttack()
    {
        flipped = (Mathf.Abs(Mathf.Atan2(mouseRelativeToPlayer.y, mouseRelativeToPlayer.x) * Mathf.Rad2Deg)) < 90;
        base.StartAttack();
    }

    protected override IEnumerator Attack()
    {
        cooldownTimer = cooldown + duration + startup + recovery;
        orbit = false;
        Quaternion target;
        if (flipped)
        {
            target = Quaternion.Euler(new Vector3(0, 0, axis.z - recoilDistance));
            axis.z += windUpDistance;
        }
        else
        {
            target = Quaternion.Euler(new Vector3(0, 0, axis.z + recoilDistance));
            axis.z -= windUpDistance;
        }
        rotationPoint.rotation = Quaternion.Euler(axis);
        yield return new WaitForSeconds(startup);
        float counter = 0;
        Quaternion current = rotationPoint.rotation;
        ActivateSensor();

        SoundMaker.instance.PlayerSwordAttackSound(gameObject.transform.position);

        while (counter < duration)
        {
            counter += Time.deltaTime;
            rotationPoint.rotation = Quaternion.Lerp(current, target, counter / duration);
            yield return null;
        }
        DeactivateSensor();
        yield return new WaitForSeconds(recovery);
        orbit = true;
    }

    protected override void EndAttackSpecifics()
    {
        cooldownTimer = 0;
        DeactivateSensor();
    }
}
