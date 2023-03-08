using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int ROTATION_OFFSET = 45;
    protected Vector3 axis;
    protected Vector3 mouseRelativeToPlayer;
    protected Vector3 objectWorldPosition;
    protected Transform rotationPoint;
    [SerializeField] protected bool orbit = true;
    [Header("Parameters")]
    [SerializeField] protected float baseDamage;
    [SerializeField] protected float defaultStartup;
    [SerializeField] protected float defaultRecovery;
    [SerializeField] protected float cooldown;
    [SerializeField] protected float cooldownTimer;

    protected float startup;
    protected float recovery;

    private Coroutine attack = null;
    protected PlayerBaseWeaponStat playerBaseWeaponStat = null;

    protected virtual void Start()
    {
        //SetDefault(true);
    }

    public virtual void SetDefault(bool defaultTimers)
    {
        rotationPoint = transform.parent;
        if (!defaultTimers) return;
        CalculateNewSpeed();
    }

    public virtual void SetPlayerBaseWeaponStat(PlayerBaseWeaponStat playerBaseWeaponStat)
    {
        this.playerBaseWeaponStat = playerBaseWeaponStat;
    }

    protected PlayerBaseWeaponStat GetWeaponBaseStats()
    {
        return playerBaseWeaponStat;
    }

    protected virtual void Update()
    {
        if (rotationPoint == null) return;
        CalculateMouseRelativeToPlayer();
        objectWorldPosition = Camera.main.WorldToScreenPoint(rotationPoint.position);
        if (orbit)
            OrbitClosestToMouse();
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Fire1"))
        {
            StartAttack();
        }
    }

    void CalculateMouseRelativeToPlayer()
    {
        mouseRelativeToPlayer = Input.mousePosition;
        mouseRelativeToPlayer.x -= objectWorldPosition.x;
        mouseRelativeToPlayer.y -= objectWorldPosition.y;
        mouseRelativeToPlayer.z = 0;
    }

    void OrbitClosestToMouse()
    {
        float angle = Mathf.Atan2(mouseRelativeToPlayer.y, mouseRelativeToPlayer.x) * Mathf.Rad2Deg - ROTATION_OFFSET;
        axis.z = angle;
        rotationPoint.rotation = Quaternion.Euler(axis);
    }

    public virtual void StartAttack()
    {
        if (cooldownTimer <= 0)
        {
            attack = StartCoroutine(Attack());
        }
    }

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

    private void OnEnable()
    {
        orbit = true;
    }

    public virtual void CalculateNewSpeed()
    {
        Debug.Log(playerBaseWeaponStat);
        float speedLevel = playerBaseWeaponStat.GetBaseSpeedLevel();

        Debug.Log(speedLevel);
        startup = defaultStartup * (1 - (speedLevel * 0.05f));
        recovery = defaultRecovery * (1 - (speedLevel * 0.05f));

        if (startup < 0.01f) startup = 0.01f;
        if (recovery < 0.01f) recovery = 0.01f;
        Debug.Log("startup: " + startup.ToString());
        Debug.Log("recovery: " + recovery.ToString());
    }

    protected abstract IEnumerator Attack();
    protected abstract void EndAttackSpecifics();
}
