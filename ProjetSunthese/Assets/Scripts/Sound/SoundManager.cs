using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;
    [Header("Tuto panel")]
    [SerializeField] private AudioClip panelPopUp;

    [Header("Shop")]
    [SerializeField] private AudioClip buyItem;
    [SerializeField] private AudioClip denyBuyItem;
    
    [Header("Player")]
    [SerializeField] private AudioClip playerRoll;
    [SerializeField] private AudioClip playerBowAttack;
    [SerializeField] private AudioClip playerAxeAttack;
    [SerializeField] private AudioClip playerWandAttack;
    [SerializeField] private AudioClip playerStaffAttack;
    [SerializeField] private AudioClip playerDaggerAttack;
    [SerializeField] private AudioClip playerSwordAttack;
    [SerializeField] private AudioClip playerDie;
    [SerializeField] private AudioClip playerWalk;
    [SerializeField] private AudioClip playerTakeDamage;

    [Header("Boss - Gontrand (Lava)")]
    [SerializeField] private AudioClip gontrandShockWave;
    [SerializeField] private AudioClip gontrandAura;
    [SerializeField] private AudioClip gontrandDie;

    [Header("Boss - Micheal (Snow)")]
    [SerializeField] private AudioClip michealTP;
    [SerializeField] private AudioClip michealThrow;
    [SerializeField] private AudioClip michealDie;

    [Header("Boss - Jean-Guy (Mountain)")]
    [SerializeField] private AudioClip jgRockThrow;
    [SerializeField] private AudioClip jgSpawnStalagmites;
    [SerializeField] private AudioClip jgSpawnMobs;
    [SerializeField] private AudioClip jgDie;

    [Header("Boss - Bob (Dungeon)")]
    [SerializeField] private AudioClip bobAura;
    [SerializeField] private AudioClip bobLaser;
    [SerializeField] private AudioClip bobFires;
    [SerializeField] private AudioClip bobInvincibility;
    [SerializeField] private AudioClip bobDie;

    [Header("Boss - Bofrer")]
    [SerializeField] private AudioClip bofrerBFL;
    [SerializeField] private AudioClip bofrerRockets;
    [SerializeField] private AudioClip bofrerShieldHitSound;
    [SerializeField] private AudioClip bofrerHomingBall;
    [SerializeField] private AudioClip bofrerStolenAttackSound;

    [Header("Miscellaneous")]
    [SerializeField] private AudioClip heal;
    [SerializeField] private AudioClip useItem;
    [SerializeField] private AudioClip levelUp;
    [SerializeField] private AudioClip victory;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private AudioClip enemyDeath;

    public static SoundManager Instance { get { return instance; } }

    public AudioClip PanelPopUp { get => panelPopUp; }
    public AudioClip BuyItem { get => buyItem; }
    public AudioClip DenyBuyItem { get => denyBuyItem; }
    public AudioClip PlayerRoll { get => playerRoll; }
    public AudioClip PlayerBowAttack { get => playerBowAttack; }
    public AudioClip PlayerAxeAttack { get => playerAxeAttack; }
    public AudioClip PlayerWandAttack { get => playerWandAttack; }
    public AudioClip PlayerStaffAttack { get => playerStaffAttack; }
    public AudioClip PlayerDaggerAttack { get => playerDaggerAttack; }
    public AudioClip PlayerSwordAttack { get => playerSwordAttack;}
    public AudioClip PlayerDie { get => playerDie; }
    public AudioClip PlayerWalk { get => playerWalk; }
    public AudioClip PlayerTakeDamage { get => playerTakeDamage;}
    public AudioClip GontrandShockWave { get => gontrandShockWave; }
    public AudioClip GontrandAura { get => gontrandAura; }
    public AudioClip GontrandDie { get => gontrandDie; }
    public AudioClip MichealTP { get => michealTP; }
    public AudioClip MichealThrow { get => michealThrow; }
    public AudioClip MichealDie { get => michealDie; }
    public AudioClip JgRockThrow { get => jgRockThrow;}
    public AudioClip JgSpawnStalagmites { get => jgSpawnStalagmites; }
    public AudioClip JgSpawnMobs { get => jgSpawnMobs; }
    public AudioClip JgDie { get => jgDie; }
    public AudioClip BobAura { get => bobAura; }
    public AudioClip BobLaser { get => bobLaser;}
    public AudioClip BobInvincibility { get => bobInvincibility; }
    public AudioClip BobDie { get => bobDie; }
    public AudioClip BobFires { get => bobFires; }
    public AudioClip BofrerBFL { get => bofrerBFL; }
    public AudioClip BofrerRockets { get => bofrerRockets; }
    public AudioClip BofrerShieldHitSound { get => bofrerShieldHitSound; }
    public AudioClip BofrerHomingBall { get => bofrerHomingBall; }
    public AudioClip BofrerStolenAttackSound { get => bofrerStolenAttackSound; }
    public AudioClip Heal { get => heal; }
    public AudioClip UseItem { get => useItem; }
    public AudioClip LevelUp { get => levelUp; }
    public AudioClip Victory { get => victory; }
    public AudioClip GameOver { get => gameOver; }
    public AudioClip EnemyDeath { get => enemyDeath; }

    void Awake()
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
