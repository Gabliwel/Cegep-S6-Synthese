using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int ROTATION_OFFSET = 45;
    protected Vector3 axis;
    protected Vector3 mousePosition;
    protected Vector3 objectWorldPosition;
    protected Transform rotationPoint;
    [SerializeField] protected bool orbit = true;
    [Header("Parameters")]
    [SerializeField] protected float damage;
    [SerializeField] protected float startup;
    [SerializeField] protected float recovery;
    [SerializeField] protected float cooldown;
    [SerializeField] protected float cooldownTimer;


    protected virtual void Awake()
    {
        rotationPoint = transform.parent;
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
            StartAttack();
        }
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

    protected void StartAttack()
    {
        if (cooldownTimer <= 0)
        {
            StartCoroutine(Attack());
        }
    }

    protected abstract IEnumerator Attack();
}
