using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Billy.Weapons;

public class Player : MonoBehaviour
{
    public static Player instance;
    private PlayerAnimationController animationController;
    private PlayerMovement playerMovement;
    private Weapon weapon;
    private WeaponInformations weaponInfo;
    private Inventory inventory;
    private PlayerLight playerLight;
    private ProjectilesManager projectilesManager;
    private PlayerHealth health;
    private PlayerBaseWeaponStat baseWeaponStat;
    private SpriteRenderer sprite;
    private PlayerInteractables playerInteractables;
    private ParticleSystem fireParticle;
    private float iframesTimer;

    [Header("Link")]
    [SerializeField] private GameObject stimuli;
    [SerializeField] private GameObject sensor;
    [SerializeField] private GameObject particuleGameObj;

    [Header("Ressources")]
    [SerializeField] private int gold = 0;
    [SerializeField] private float levelUpAugmentationRate = 1.4f;
    [SerializeField] private int neededXp = 100;
    [SerializeField] private int currentXp = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private float luck = 0;

    public int Gold { get => gold; }
    public int CurrentXp { get => currentXp; }
    public float Health { get => health.CurrentHealth; }
    public float Luck { get => luck; }

    private bool bloodSuck = false;
    private float bloodSuckRate = 0;

    private bool crazyHeart = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        health = GetComponent<PlayerHealth>();
        animationController = GetComponent<PlayerAnimationController>();
        playerMovement = GetComponent<PlayerMovement>();
        weapon = GetComponentInChildren<Weapon>();
        weaponInfo = weapon.gameObject.GetComponent<WeaponInformations>();
        playerLight = GetComponentInChildren<PlayerLight>();
        baseWeaponStat = GetComponent<PlayerBaseWeaponStat>();
        playerInteractables = GetComponent<PlayerInteractables>();
        sprite = GetComponent<SpriteRenderer>();
        fireParticle = particuleGameObj.GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        weapon.SetPlayerBaseWeaponStat(baseWeaponStat);
        weapon.CalculateNewSpeed();
        animationController.ChangeOnWeaponType(weaponInfo.GetWeaponType());
    }

    private void Update()
    {
        if (iframesTimer > 0)
            iframesTimer -= Time.deltaTime;
    }

    public void UpdateInteractables(Interactable interectable)
    {
        playerInteractables.SearchNewInterac(interectable);
    }

    public void AddIframes(float ammount)
    {
        iframesTimer += ammount;
    }


    #region Health
    public void MaxHealthBoost(float value)
    {
        health.AddMaxHp(value);
        GameManager.instance.UpdateHUD();
    }

    public void HealPercent(float healingPercent)
    {
        health.HealPercent(healingPercent);
        GameManager.instance.UpdateHUD();
    }

    public void GainArmor(float value)
    {
        health.GainArmor(value);
    }

    public void GainStoneHeart()
    {
        health.GainStoneHeart();
        GameManager.instance.UpdateHUD();
    }

    public void GainSecondChance()
    {
        health.GainSecondChance();
    }

    public void GainDeathContract()
    {
        health.GainDeathContract();
    }

    #endregion

    public void GetCrazyHalfHeart()
    {
        if(!crazyHeart)
        {
            baseWeaponStat.MultiplyBaseAttack(1.5f);
        }
        else
        {
            crazyHeart = true;
            baseWeaponStat.MultiplyBaseAttack(2f);
        }
        health.IncreaseReceiveDamageMultiplicator();
    }

    public void GainBloodSuck()
    {
        bloodSuck = true;
        bloodSuckRate += 0.5f;
    }

    public void HealBloodSuck()
    {
        if (bloodSuck)
        {
            health.HealSpecific(bloodSuckRate);
        }
        
    }

    public void GainXp(int amount)
    {
        currentXp += amount;
        if (currentXp >= neededXp)
        {
            currentXp -= neededXp;
            neededXp = (int)Math.Round(neededXp * levelUpAugmentationRate);
            level++;
            BoostDamage(0.15f);
            MaxHealthBoost(10);
            GameManager.instance.UpdateHUD();
        }
    }

    public void BoostDamage(float value)
    {
        baseWeaponStat.IncreaseBaseAttack(value);
    }

    public void BoostPlayerSpeed(float value)
    {
        playerMovement.IncreaseBaseSpeed(value);
    }

    public void IncreasePlayerLuck(float value)
    {
        if (luck < 90) luck += value;
    }

    public void IncreaseAttackSpeed(float lvl)
    {
        baseWeaponStat.IncreaseSpeedLevel(lvl);
        weapon.CalculateNewSpeed();
    }

    public void AddPoison(int lvl)
    {
        baseWeaponStat.IncreasePoisonLevel(lvl);
    }

    public void GainGold(int amount)
    {
        gold += amount;
        GameManager.instance.UpdateHUD();
    }

    public void GainDrops(int xp, int gold)
    {
        HealBloodSuck();
        GainXp(xp);
        GainGold(gold);
        GameManager.instance.UpdateHUD();
    }

    public bool BuyItem(int price)
    {
        if (gold >= price)
        {
            gold -= price;
            GameManager.instance.UpdateHUD();
            return true;
        }
        return false;
    }

    public bool Harm(float ammount)
    {
        if (iframesTimer <= 0)
        {
            health.Harm(ammount);
            GameManager.instance.UpdateHUD();
            DamageNumbersManager.instance.CallText(ammount, transform.position, true);
            return true;
        }
        return false;
    }

    public void HarmIgnoreIFrame(float ammount)
    {
        health.Harm(ammount);
        GameManager.instance.UpdateHUD();
    }

    public void ChangeLayer(string layer, string sortingLayer)
    {
        gameObject.layer = LayerMask.NameToLayer(layer);
        stimuli.layer = LayerMask.NameToLayer(layer);
        sensor.layer = LayerMask.NameToLayer(layer);
        sprite.sortingLayerName = sortingLayer;
        weaponInfo.ChangeLayer(layer, sortingLayer);
        playerLight.UpdateLightUsage(sortingLayer);
        fireParticle.gameObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = sortingLayer;
    }

    public void BlocMovement(bool state)
    {
        if (state) playerMovement.DisableMovement();
        else playerMovement.EnableMovement();
    }

    public void BlocAttack(bool state)
    {
        weapon.gameObject.SetActive(!state);
    }

    public void SetProjectilesManager(ProjectilesManager newProjectilesManager)
    {
        projectilesManager = newProjectilesManager;
        SetCurrentWeapon();
    }

    private void SetCurrentWeapon()
    {
        if (weaponInfo.GetWeaponType() == WeaponsType.BOW)
            weapon.gameObject.GetComponent<Bow>().SetProjectiles(projectilesManager.GetArrows());

        if (weaponInfo.GetWeaponType() == WeaponsType.WARLORCK_STAFF)
            weapon.gameObject.GetComponent<WarlorckStaff>().SetProjectiles(projectilesManager.GetWarlockProjectiles());
        if (weaponInfo.GetWeaponType() == WeaponsType.STAFF)
            weapon.gameObject.GetComponent<Staff>().SetProjectiles(projectilesManager.GetWizardProjectiles());
    }

    public void SwitchWeapon(GameObject newWeapon)
    {
        Vector3 originalPosition = newWeapon.transform.position;

        weapon.EndAttack();

        Transform weaponParent = weapon.transform.parent;
        weapon.transform.parent = null;

        WeaponInformations oldWeaponInfo = weapon.GetComponent<WeaponInformations>();
        oldWeaponInfo.gameObject.transform.position = originalPosition;
        oldWeaponInfo.gameObject.transform.localScale = oldWeaponInfo.GetScaleNotWithPlayer();
        oldWeaponInfo.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
        oldWeaponInfo.SwitchToInteractable();

        weapon = newWeapon.GetComponent<Weapon>();

        newWeapon.transform.SetParent(weaponParent.transform, true);
        WeaponInformations newWeaponInfo = newWeapon.GetComponent<WeaponInformations>();
        weaponInfo = newWeaponInfo;

        newWeapon.transform.localPosition = newWeaponInfo.GetPositionWithPlayer();
        newWeapon.transform.localScale = new Vector3(1, 1, 1);
        weapon.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);

        newWeaponInfo.SwitchToWeapon();
        weapon.SetPlayerBaseWeaponStat(baseWeaponStat);
        weapon.SetDefault();
        weapon.CalculateNewSpeed();

        //change anim et autre...
        animationController.ChangeOnWeaponType(weaponInfo.GetWeaponType());
        SetCurrentWeapon();
    }

    public void KnockBack(Vector2 difference, float force)
    {
        if (iframesTimer <= 0)
        {
            playerMovement.AddKnockBack(difference, force);
        }
    }

    public void IsInLava(float speedReducer)
    {
        fireParticle.Play();
        playerMovement.SetSpeedReducer(speedReducer);
    }

    public void IsNotInLava()
    {
        fireParticle.Stop(false, ParticleSystemStopBehavior.StopEmitting);
        playerMovement.SetSpeedReducer(1);
    }

    [ContextMenu("NextLevel")]
    public void NextLevel()
    {
        GameManager.instance.GetRandomNextLevelAndStart();
    }

    [ContextMenu("Central")]
    public void BackToMain()
    {
        GameManager.instance.GetBackToMainStageAndStart();
    }

    [ContextMenu("KevLevel")]
    public void KevLevel()
    {
        GameManager.instance.StartNextlevel(0, Scene.KevenLevel);
    }

}
