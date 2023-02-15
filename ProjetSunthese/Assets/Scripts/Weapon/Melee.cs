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
    private Sensor sensor;
    private ISensor<Enemy> enemySensor;

    protected override void Awake()
    {
        base.Awake();
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
    protected override IEnumerator Attack()
    {
        Vector3 worldPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.x -= worldPosition.x;
        mousePosition.y -= worldPosition.y;
        mousePosition.z = 0;

        bool flipped = (Mathf.Abs(Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg)) > 90;

        cooldownTimer = cooldown + duration + startup + recovery;
        orbit = false;
        if (flipped)
            axis.z -= windUpDistance;
        else
            axis.z += windUpDistance;

        rotationPoint.rotation = Quaternion.Euler(axis);
        yield return new WaitForSeconds(startup);
        float stopTime = Time.time + duration;
        ActivateSensor();
        while (Time.time < stopTime)
        {
            yield return new WaitForEndOfFrame();
            if (flipped)
                axis.z += (recoilDistance + windUpDistance) / duration * Time.deltaTime;
            else
                axis.z -= (recoilDistance + windUpDistance) / duration * Time.deltaTime;
            rotationPoint.rotation = Quaternion.Euler(axis);
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
