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
        SetDefault();
    }

    public void SetDefault()
    {
        rotationPoint = transform.parent;
        startup = defaultStartup;
        recovery = defaultRecovery;
    }

    public void SetPlayerBaseWeaponStat(PlayerBaseWeaponStat playerBaseWeaponStat)
    {
        this.playerBaseWeaponStat = playerBaseWeaponStat;
    }

    protected PlayerBaseWeaponStat GetWeaponBaseStats()
    {
        return playerBaseWeaponStat;
    }

    protected virtual void Update()
    {
        CalculateMouseRelativeToPlayer();
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

    protected void StartAttack()
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

    public void CalculateNewSpeed()
    {
        int speedLevel = playerBaseWeaponStat.GetBaseSpeedLevel();
        startup = defaultStartup * (1 - speedLevel * 0.05f);
        recovery = defaultRecovery * (1 - speedLevel * 0.05f);

        if (startup < 0.01f) startup = 0.01f;
        if (recovery < 0.01f) recovery = 0.01f;
    }

    protected abstract IEnumerator Attack();
    protected abstract void EndAttackSpecifics();
}
