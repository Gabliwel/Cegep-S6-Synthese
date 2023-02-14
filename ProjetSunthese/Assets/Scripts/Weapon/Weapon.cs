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
    [SerializeField] protected float poisonDamage;

    protected bool doubleNumber;

    protected float defaultStartup;
    protected float defaultRecovery;

    private Coroutine attack = null;

    protected virtual void Awake()
    {
        SetDefault();
    }

    public void SetDefault()
    {
        rotationPoint = transform.parent;
        defaultStartup = startup;
        defaultRecovery = recovery;
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
            attack = StartCoroutine(Attack());
        }
    }

    protected abstract void EndAttackSpecifics();

    public void EndAttack()
    {
        if(attack != null)
        {
            StopCoroutine(attack);
            attack = null;

            EndAttackSpecifics();
            orbit = true;
        }
    }

    public void GainDoubleNumber()
    {
        doubleNumber = true;
    }

    public void GainSpeed(float speed)
    {
        startup = defaultStartup * (1 - speed * 0.05f);
        recovery = defaultRecovery * (1 - speed * 0.05f);
    }

    public void GainPoison()
    {
        poisonDamage += 5;
    }

    public void AddDamage()
    {
        damage++;
    }

   /* public void SwitchWeapon(int weaponNb)
    {
        int currentDamageBoost = player.GetDamageBoost();
        float currentSpeed = player.GetAttackSpeed();
        float speedIncrease = currentSpeed * 5 / 100;

        poisonDamage = player.GetPoisonDamage();
        doubleNumber = player.CheckDouble();

        switch (weaponNb)
        {
            case 1:
                Debug.Log("Sword");
                damage = 10 + currentDamageBoost;
                defaultRecovery = 0.2f;
                defaultStartup = 0.15f;

                startup = defaultStartup - speedIncrease * defaultStartup;
                recovery = defaultRecovery - speedIncrease * defaultRecovery;
                GetComponent<SpriteRenderer>().sprite = sword;
                break;
            case 2:
                Debug.Log("Axe");
                damage = 15 + currentDamageBoost;
                defaultRecovery = 0.5f;
                defaultStartup = 0.35f;

                startup = defaultStartup - speedIncrease * defaultStartup;
                recovery = defaultRecovery - speedIncrease * defaultRecovery;
                GetComponent<SpriteRenderer>().sprite = axe;
                break;
            case 3:
                Debug.Log("Dagger");
                damage = 3 + currentDamageBoost;
                defaultRecovery = 0.02f;
                defaultStartup = 0.015f;

                startup = defaultStartup - speedIncrease * defaultStartup;
                recovery = defaultRecovery - speedIncrease * defaultRecovery;
                GetComponent<SpriteRenderer>().sprite = dagger;
                break;
            case 4:
                Debug.Log("Bow");
                damage = 5 + currentDamageBoost;
                defaultRecovery = 0.1f;
                defaultStartup = 0.2f;

                startup = defaultStartup - speedIncrease * defaultStartup;
                recovery = defaultRecovery - speedIncrease * defaultRecovery;
                break;
        }
    }*/

    protected abstract IEnumerator Attack();
}
