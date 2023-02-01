using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private const int ROTATION_OFFSET = 45;
    Vector3 axis;
    Vector3 mousePosition;
    Vector3 objectWorldPosition;
    Transform rotationPoint;
    [SerializeField] bool orbit = true;
    [Header("Parameters")]
    [SerializeField] private float damage;
    [SerializeField] private float startup;
    [SerializeField] private float duration;
    [SerializeField] private float recovery;
    [SerializeField] private float cooldown;
    [Header("Rotation")]
    [Range(0, 45)]
    [SerializeField] private float windUpDistance;
    [Range(0, 45)]
    [SerializeField] private float recoilDistance;
    [SerializeField] private float cooldownTimer;
    private Sensor sensor;
    private ISensor<Enemy> enemySensor;

    private void Awake()
    {
        sensor = GetComponentInChildren<Sensor>();
        enemySensor = sensor.For<Enemy>();
        rotationPoint = transform.parent;
        enemySensor.OnSensedObject += OnEnemySense;
        enemySensor.OnUnsensedObject += OnEnemyUnsense;
        DeactivateSensor();
    }

    void Update()
    {
        mousePosition = Input.mousePosition;
        objectWorldPosition = Camera.main.WorldToScreenPoint(rotationPoint.position);
        if (orbit)
            OrbitClosestToMouse();
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }
    }
    private void OnEnemyUnsense(Enemy otherObject)
    {
        Debug.Log(otherObject.name + "unsensed");
    }

    private void OnEnemySense(Enemy otherObject)
    {
        Debug.Log(otherObject.name + "sensed");
    }

    void OrbitClosestToMouse()
    {
        Vector3 newMousePosition = mousePosition;
        newMousePosition.x -= objectWorldPosition.x;
        newMousePosition.y -= objectWorldPosition.y;
        newMousePosition.z = 0;

        float angle = Mathf.Atan2(newMousePosition.y, newMousePosition.x) * Mathf.Rad2Deg - ROTATION_OFFSET;
        axis.z = angle;
        rotationPoint.rotation = Quaternion.Euler(axis);
    }

    public void Attack()
    {
        if (cooldownTimer <= 0)
        {
            StartCoroutine(Swing());
        }
    }

    protected virtual IEnumerator Swing()
    {
        cooldownTimer = cooldown + duration + startup + recovery;
        orbit = false;
        axis.z += windUpDistance;
        rotationPoint.rotation = Quaternion.Euler(axis);
        yield return new WaitForSeconds(startup);
        float stopTime = Time.time + duration;
        ActivateSensor();
        while (Time.time < stopTime)
        {
            yield return new WaitForEndOfFrame();
            axis.z -= (recoilDistance + windUpDistance) / duration * Time.deltaTime;
            rotationPoint.rotation = Quaternion.Euler(axis);
            yield return null;
        }
        DeactivateSensor();
        yield return new WaitForSeconds(recovery);
        orbit = true;
    }

    void ActivateSensor()
    {
        sensor.gameObject.SetActive(true);
    }
    void DeactivateSensor()
    {
        sensor.gameObject.SetActive(false);
    }
}
